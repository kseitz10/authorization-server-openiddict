using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthorizationServer.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string UserId { get; }
    }
}