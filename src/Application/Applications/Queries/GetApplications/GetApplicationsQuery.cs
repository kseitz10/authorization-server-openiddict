using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AuthorizationServer.Application.Common.Security;

using MediatR;

using OpenIddict.Abstractions;

namespace AuthorizationServer.Application.Applications.Queries.GetApplications
{
    [Authorize(Roles = UserRoles.ApplicationAdministrator)]
    public class GetApplicationsQuery : IRequest<IList<OpenIddictApplicationDescriptor>>
    {
    }

    public class GetApplicationsQueryHandler : IRequestHandler<GetApplicationsQuery, IList<OpenIddictApplicationDescriptor>>
    {
        private readonly IOpenIddictApplicationManager _appManager;

        public GetApplicationsQueryHandler(IOpenIddictApplicationManager appManager)
        {
            _appManager = appManager;
        }

        public async Task<IList<OpenIddictApplicationDescriptor>> Handle(GetApplicationsQuery request, CancellationToken cancellationToken)
        {
            var rtn = new List<OpenIddictApplicationDescriptor>();
            await foreach (var entity in _appManager.ListAsync(_ => _, cancellationToken))
            {
                var descriptor = new OpenIddictApplicationDescriptor();
                await _appManager.PopulateAsync(descriptor, entity, cancellationToken);
                rtn.Add(descriptor);
            }

            return rtn;
        }
    }
}