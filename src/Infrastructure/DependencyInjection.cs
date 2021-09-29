using System;
using System.Collections.Generic;
using System.Linq;

using AuthorizationServer.Application.Common.Interfaces;
using AuthorizationServer.Infrastructure.Identity;
using AuthorizationServer.Infrastructure.Persistence;
using AuthorizationServer.Infrastructure.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AuthorizationServer.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, Action<IdentityBuilder> identityCustomization = null, Action<IdentityOptions> identityOptionsCustomization = null)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                if (configuration.GetValue<bool>("UseInMemoryDatabase"))
                {
                    options.UseInMemoryDatabase("AuthorizationServerDb");
                }
                else
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                                         b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                }

                options.UseOpenIddict();
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            services.AddScoped<IDomainEventService, DomainEventService>();

            var identityBuilder = services.AddIdentity<ApplicationUser, IdentityRole>(opts => identityOptionsCustomization?.Invoke(opts))
                                          .AddEntityFrameworkStores<ApplicationDbContext>()
                                          .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);
            identityCustomization?.Invoke(identityBuilder);

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIdentityService, IdentityService>();

            services.AddAuthorization(options => { options.AddPolicy("CanPurge", policy => policy.RequireRole("Administrator")); });

            services.AddOpenIddict()
                    .AddCore(options => { options.UseEntityFrameworkCore().UseDbContext<ApplicationDbContext>(); });

            return services;
        }
    }
}