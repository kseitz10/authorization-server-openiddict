using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AuthorizationServer.Application.Applications.Queries.GetApplications;

using OpenIddict.Abstractions;

namespace AuthorizationServer.WebUI.Pages.Applications
{
    public class IndexModel : PageBase
    {
        public IList<OpenIddictApplicationDescriptor> OpenIddictApplications { get; set; }

        public async Task OnGetAsync()
        {
            OpenIddictApplications = await Mediator.Send(new GetApplicationsQuery());
        }
    }
}