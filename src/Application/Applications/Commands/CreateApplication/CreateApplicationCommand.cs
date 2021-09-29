using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

using AuthorizationServer.Application.Common.Security;

using MediatR;

using OpenIddict.Abstractions;

namespace AuthorizationServer.Application.Applications.Commands.CreateApplication
{
    [Authorize(Roles = UserRoles.ApplicationAdministrator)]
    public class CreateApplicationCommand : IRequest<string>
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

    public class CreateApplicationCommandHandler : IRequestHandler<CreateApplicationCommand, string>
    {
        private readonly IOpenIddictApplicationManager _appManager;

        public CreateApplicationCommandHandler(IOpenIddictApplicationManager appManager)
        {
            _appManager = appManager;
        }

        public async Task<string> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
        {
            // TODO Set additional properties
            var descriptor = new OpenIddictApplicationDescriptor
            {
                ClientId = request.ClientId,
                ClientSecret = request.ClientSecret,
                DisplayName = request.DisplayName,
                RedirectUris = { new Uri("https://oauth.pstmn.io/v1/callback") },
                Permissions =
                {
                    OpenIddictConstants.Permissions.Endpoints.Authorization,
                    OpenIddictConstants.Permissions.Endpoints.Token,

                    OpenIddictConstants.Permissions.GrantTypes.AuthorizationCode,
                    OpenIddictConstants.Permissions.GrantTypes.ClientCredentials,
                    OpenIddictConstants.Permissions.GrantTypes.RefreshToken,

                    OpenIddictConstants.Permissions.Prefixes.Scope + "api",
                    OpenIddictConstants.Permissions.ResponseTypes.Code
                }
            };

            return (await _appManager.CreateAsync(descriptor, cancellationToken)).ToString();
        }
    }
}