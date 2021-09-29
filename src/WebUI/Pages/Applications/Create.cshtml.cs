using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AuthorizationServer.Application.Applications.Commands.CreateApplication;

using Microsoft.AspNetCore.Mvc;

namespace AuthorizationServer.WebUI.Pages.Applications
{
    public class CreateModel : PageBase
    {
        [BindProperty]
        public CreateApplicationCommand OpenIddictApplication { get; set; } = new();

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await Mediator.Send(OpenIddictApplication);

            return RedirectToPage("./Index");
        }
    }
}