using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

using AuthorizationServer.Application.Common.Interfaces;

using Microsoft.AspNetCore.Http;

namespace AuthorizationServer.WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}