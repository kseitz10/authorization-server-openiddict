using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.RazorPages;

using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;

namespace AuthorizationServer.WebUI.Pages.Applications
{
    public class IndexModel : PageModel
    {
        private readonly OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication> _appManager;

        public IndexModel(OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication> appManager)
        {
            _appManager = appManager;
        }

        public IList<OpenIddictEntityFrameworkCoreApplication> OpenIddictApplication { get;set; }

        public async Task OnGetAsync()
        {
            OpenIddictApplication = new List<OpenIddictEntityFrameworkCoreApplication>();
            await foreach (var blah in _appManager.ListAsync(_ => _))
            {
                OpenIddictApplication.Add(blah);
            }
        }
    }
}
