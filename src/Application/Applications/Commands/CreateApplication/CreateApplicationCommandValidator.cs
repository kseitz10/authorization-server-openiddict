using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using FluentValidation;

using OpenIddict.Abstractions;

namespace AuthorizationServer.Application.Applications.Commands.CreateApplication
{
    public class CreateApplicationCommandValidator : AbstractValidator<CreateApplicationCommand>
    {
        private readonly IOpenIddictApplicationManager _appManager;

        public CreateApplicationCommandValidator(IOpenIddictApplicationManager appManager)
        {
            _appManager = appManager;

            RuleFor(v => v.ClientId)
                .NotEmpty().WithMessage("Client ID is required.")
                .MaximumLength(100).WithMessage("Client ID must not exceed 100 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("The specified Client ID already exists.");
        }

        public async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            var matchingClientId = await _appManager.FindByClientIdAsync(title, cancellationToken);
            return matchingClientId == null;
        }
    }
}