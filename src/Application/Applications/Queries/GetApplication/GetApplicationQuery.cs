using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AuthorizationServer.Application.Common.Exceptions;

using MediatR;

using OpenIddict.Abstractions;

namespace AuthorizationServer.Application.Applications.Queries.GetApplication
{
    public class GetApplicationQuery : IRequest<OpenIddictApplicationDescriptor>
    {
        public GetApplicationQuery(string clientId)
        {
            ClientId = clientId;
        }

        public string ClientId { get; set; }
    }

    public class GetApplicationQueryHandler : IRequestHandler<GetApplicationQuery, OpenIddictApplicationDescriptor>
    {
        private readonly IOpenIddictApplicationManager _appManager;

        public GetApplicationQueryHandler(IOpenIddictApplicationManager appManager)
        {
            _appManager = appManager;
        }

        public async Task<OpenIddictApplicationDescriptor> Handle(GetApplicationQuery request, CancellationToken cancellationToken)
        {
            var entity = await _appManager.FindByClientIdAsync(request.ClientId, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(OpenIddictApplicationDescriptor), request.ClientId);
            }

            var descriptor = new OpenIddictApplicationDescriptor();
            await _appManager.PopulateAsync(descriptor, entity, cancellationToken);

            return descriptor;
        }
    }
}