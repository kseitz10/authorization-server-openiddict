using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AuthorizationServer.Application.Applications.Commands.DeleteApplication;
using AuthorizationServer.Application.Applications.Queries.GetApplication;

using Microsoft.AspNetCore.Mvc;

using OpenIddict.Abstractions;

namespace AuthorizationServer.WebUI.Pages.Applications
{
    public class DeleteModel : PageBase
    {
        [BindProperty]
        public OpenIddictApplicationDescriptor OpenIddictApplication { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OpenIddictApplication = await Mediator.Send(new GetApplicationQuery(id));

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            await Mediator.Send(new DeleteApplicationCommand(id));

            return RedirectToPage("./Index");
        }
    }
}