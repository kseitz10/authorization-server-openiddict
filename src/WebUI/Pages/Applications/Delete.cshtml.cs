using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using OpenIddict.Abstractions;
using OpenIddict.Core;
using OpenIddict.EntityFrameworkCore.Models;

namespace AuthorizationServer.WebUI.Pages.Applications
{
    public class DeleteModel : PageModel
    {
        private readonly OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication> _appManager;

        public DeleteModel(OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication> appManager)
        {
            _appManager = appManager;
        }

        [BindProperty]
        public OpenIddictApplicationDescriptor OpenIddictApplication { get; set; }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null || OpenIddictApplication == null)
            {
                return NotFound();
            }

            var existing = await _appManager.FindByIdAsync(id);
            if (existing == null)
            {
                return NotFound();
            }

            await _appManager.DeleteAsync(existing);

            return RedirectToPage("./Index");
        }
    }
}
