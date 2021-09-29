using System;
using System.Collections.Generic;
using System.Linq;

using MediatR;

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.DependencyInjection;

namespace AuthorizationServer.WebUI.Pages
{
    public abstract class PageBase : PageModel
    {
        private ISender _mediator;
        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    }
}