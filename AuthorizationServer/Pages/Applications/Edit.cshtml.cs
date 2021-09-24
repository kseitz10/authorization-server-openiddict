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
    public class EditModel : PageModel
    {
        private readonly OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication> _appManager;

        public EditModel(OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication> appManager)
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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (OpenIddictApplication == null || string.IsNullOrEmpty(Id))
            {
                return NotFound();
            }

            var existing = await _appManager.FindByIdAsync(Id);
            if (existing == null)
            {
                return NotFound();
            }

            await _appManager.UpdateAsync(existing);

            return RedirectToPage("./Index");
        }
    }
}