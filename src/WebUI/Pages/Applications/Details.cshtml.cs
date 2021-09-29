using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AuthorizationServer.Application.Applications.Queries.GetApplication;

using Microsoft.AspNetCore.Mvc;

using OpenIddict.Abstractions;

namespace AuthorizationServer.WebUI.Pages.Applications
{
    public class DetailsModel : PageBase
    {
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
    }
}