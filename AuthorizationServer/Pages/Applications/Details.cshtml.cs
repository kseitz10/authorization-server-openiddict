using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;

namespace AuthorizationServer.Pages.Applications
{
    public class DetailsModel : PageModel
    {
        private readonly OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication> _appManager;

        public DetailsModel(OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication> appManager)
        {
            _appManager = appManager;
        }

        public OpenIddictApplicationDescriptor OpenIddictApplication { get; set; }

        public string Id { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var efObject = await _appManager.FindByIdAsync(id);
            if (efObject == null)
            {
                return NotFound();
            }

            OpenIddictApplication = new OpenIddictApplicationDescriptor();
            Id = efObject.Id;
            await _appManager.PopulateAsync(OpenIddictApplication, efObject);

            return Page();
        }
    }
}