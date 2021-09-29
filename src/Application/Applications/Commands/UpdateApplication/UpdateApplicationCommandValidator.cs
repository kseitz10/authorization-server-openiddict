using System;
using System.Collections.Generic;
using System.Linq;

using FluentValidation;

namespace AuthorizationServer.Application.Applications.Commands.UpdateApplication
{
    public class UpdateApplicationCommandValidator : AbstractValidator<UpdateApplicationCommand>
    {
        public UpdateApplicationCommandValidator()
        {
            RuleFor(v => v.ClientId)
                .NotEmpty().WithMessage("Client ID is required.")
                .MaximumLength(100).WithMessage("Client ID must not exceed 100 characters.");

            RuleFor(v => v.DisplayName)
                .NotEmpty().WithMessage("Display name is required.")
                .MaximumLength(100).WithMessage("Display name must not exceed 100 characters.");
        }
    }
}