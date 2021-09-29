using System;
using System.Collections.Generic;
using System.Linq;

using AuthorizationServer.WebUI.Identity;

using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(IdentityHostingStartup))]

namespace AuthorizationServer.WebUI.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => { });
        }
    }
}