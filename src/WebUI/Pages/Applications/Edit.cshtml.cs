using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AuthorizationServer.Application.Applications.Commands.UpdateApplication;
using AuthorizationServer.Application.Applications.Queries.GetApplication;

using AutoMapper;

using Microsoft.AspNetCore.Mvc;

namespace AuthorizationServer.WebUI.Pages.Applications
{
    public class EditModel : PageBase
    {
        private readonly IMapper _mapper;

        public EditModel(IMapper mapper)
        {
            _mapper = mapper;
        }

        [BindProperty]
        public UpdateApplicationCommand OpenIddictApplication { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            OpenIddictApplication = _mapper.Map<UpdateApplicationCommand>(await Mediator.Send(new GetApplicationQuery(id)));

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (OpenIddictApplication == null || string.IsNullOrEmpty(OpenIddictApplication.ClientId))
            {
                return NotFound();
            }

            await Mediator.Send(OpenIddictApplication);

            return RedirectToPage("./Index");
        }
    }
}