using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using AuthorizationServer.Application.Common.Exceptions;
using AuthorizationServer.Application.Common.Mappings;

using MediatR;

using OpenIddict.Abstractions;

namespace AuthorizationServer.Application.Applications.Commands.UpdateApplication
{
    public class UpdateApplicationCommand : IRequest, IMapFrom<OpenIddictApplicationDescriptor>
    {
        /// <summary>
        ///     Gets or sets the client identifier associated with the application.
        /// </summary>
        public string? ClientId { get; set; }

        /// <summary>
        ///     Gets or sets the client secret associated with the application.
        ///     Note: depending on the application manager used when creating it,
        ///     this property may be hashed or encrypted for security reasons.
        /// </summary>
        public string? ClientSecret { get; set; }

        /// <summary>
        ///     Gets or sets the consent type associated with the application.
        /// </summary>
        public string? ConsentType { get; set; }

        /// <summary>
        ///     Gets or sets the display name associated with the application.
        /// </summary>
        public string? DisplayName { get; set; }

        /// <summary>
        ///     Gets the permissions associated with the application.
        /// </summary>
        public HashSet<string> Permissions { get; } = new(StringComparer.Ordinal);

        /// <summary>
        ///     Gets the logout callback URLs associated with the application.
        /// </summary>
        public HashSet<Uri> PostLogoutRedirectUris { get; } = new();

        /// <summary>
        ///     Gets the additional properties associated with the application.
        /// </summary>
        public Dictionary<string, JsonElement> Properties { get; } = new(StringComparer.Ordinal);

        /// <summary>
        ///     Gets the callback URLs associated with the application.
        /// </summary>
        public HashSet<Uri> RedirectUris { get; } = new();

        /// <summary>
        ///     Gets the requirements associated with the application.
        /// </summary>
        public HashSet<string> Requirements { get; } = new(StringComparer.Ordinal);

        /// <summary>
        ///     Gets or sets the application type associated with the application.
        /// </summary>
        public string? Type { get; set; }
    }

    public class UpdateApplicationCommandHandler : IRequestHandler<UpdateApplicationCommand>
    {
        private readonly IOpenIddictApplicationManager _appManager;

        public UpdateApplicationCommandHandler(IOpenIddictApplicationManager appManager)
        {
            _appManager = appManager;
        }

        public async Task<Unit> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _appManager.FindByClientIdAsync(request.ClientId, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(OpenIddictApplicationDescriptor), request.ClientId);
            }

            var descriptor = new OpenIddictApplicationDescriptor();
            await _appManager.PopulateAsync(descriptor, entity, cancellationToken);

            // TODO Set additional properties
            descriptor.DisplayName = request.DisplayName;

            await _appManager.UpdateAsync(entity, descriptor, cancellationToken);
            return Unit.Value;
        }
    }
}