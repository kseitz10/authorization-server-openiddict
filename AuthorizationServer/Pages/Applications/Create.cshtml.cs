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
    public class CreateModel : PageModel
    {
        private readonly OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication> _appManager;

        public CreateModel(OpenIddictApplicationManager<OpenIddictEntityFrameworkCoreApplication> appManager)
        {
            _appManager = appManager;
        }

        [BindProperty]
        public OpenIddictApplicationDescriptor OpenIddictApplication { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _appManager.CreateAsync(OpenIddictApplication);

            return RedirectToPage("./Index");
        }
    }
}