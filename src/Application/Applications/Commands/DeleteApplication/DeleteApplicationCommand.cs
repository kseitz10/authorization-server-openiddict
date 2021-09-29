using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AuthorizationServer.Application.Common.Exceptions;

using MediatR;

using OpenIddict.Abstractions;

namespace AuthorizationServer.Application.Applications.Commands.DeleteApplication
{
    public class DeleteApplicationCommand : IRequest
    {
        public DeleteApplicationCommand(string clientId)
        {
            ClientId = clientId;
        }

        public string ClientId { get; set; }
    }

    public class DeleteApplicationCommandHandler : IRequestHandler<DeleteApplicationCommand>
    {
        private readonly IOpenIddictApplicationManager _appManager;

        public DeleteApplicationCommandHandler(IOpenIddictApplicationManager appManager)
        {
            _appManager = appManager;
        }

        public async Task<Unit> Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _appManager.FindByClientIdAsync(request.ClientId, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(OpenIddictApplicationDescriptor), request.ClientId);
            }

            await _appManager.DeleteAsync(entity, cancellationToken);
            return Unit.Value;
        }
    }
}