using System;
using System.Collections.Generic;
using System.Linq;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AuthorizationServer.WebUI.Api.Controllers
{
    [ApiController]
    [Area("api")]
    [Route("[area]/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    }
}
