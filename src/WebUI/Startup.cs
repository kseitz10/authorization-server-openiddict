using System;
using System.Collections.Generic;
using System.Linq;

using AuthorizationServer.Application;
using AuthorizationServer.Application.Common.Interfaces;
using AuthorizationServer.Infrastructure;
using AuthorizationServer.Infrastructure.Persistence;
using AuthorizationServer.WebUI.Filters;
using AuthorizationServer.WebUI.Services;

using FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AuthorizationServer.WebUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IWebHostEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure(Configuration, identity => identity.AddDefaultUI(), opts => opts.SignIn.RequireConfirmedAccount = true);

            services.AddOpenIddict()
                    .AddCore(options => { options.UseEntityFrameworkCore().UseDbContext<ApplicationDbContext>(); })
                    .AddServer(options =>
                    {
                        options
                            .AllowClientCredentialsFlow()
                            .AllowAuthorizationCodeFlow()
                            .RequireProofKeyForCodeExchange()
                            .AllowRefreshTokenFlow();

                        options
                            .SetTokenEndpointUris("/connect/token")
                            .SetAuthorizationEndpointUris("/connect/authorize")
                            .SetUserinfoEndpointUris("/connect/userinfo");

                        // Encryption and signing of tokens
                        // See https://documentation.openiddict.com/configuration/encryption-and-signing-credentials.html
#if DEBUG
                        options.AddDevelopmentEncryptionCertificate()
                               .AddDevelopmentSigningCertificate();
#else
                    // TODO This would have to be changed prior to deployment.
                    options.AddEphemeralEncryptionKey()
                           .AddEphemeralSigningKey()
                           .DisableAccessTokenEncryption();
#endif

                        // Register scopes (permissions)
                        options.RegisterScopes("api");

                        // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
                        options
                            .UseAspNetCore()
                            .EnableTokenEndpointPassthrough()
                            .EnableAuthorizationEndpointPassthrough()
                            .EnableUserinfoEndpointPassthrough();
                    })
                    .AddValidation(options =>
                    {
                        // For when the API and the authorization server are in the same project.
                        // See https://documentation.openiddict.com/configuration/encryption-and-signing-credentials.html#using-the-optionsuselocalserver-integration
                        options.UseLocalServer();
                    });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton<ICurrentUserService, CurrentUserService>();

            services.AddHttpContextAccessor();

            services.AddHealthChecks()
                    .AddDbContextCheck<ApplicationDbContext>();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => { options.LoginPath = "/identity/account/login"; });

            services.AddControllers(options =>
                                        options.Filters.Add<ApiExceptionFilterAttribute>())
                    .AddFluentValidation(x => x.AutomaticValidationEnabled = false);

#if DEBUG
            services.AddRazorPages().AddRazorRuntimeCompilation();
#else
            services.AddRazorPages();
#endif

            // Customise default API behaviour
            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

            if (Environment.IsDevelopment())
            {
                services.AddHostedService<TestData>();
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHealthChecks("/health");
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "apiArea",
                    pattern: "{area:exists}/{controller}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}