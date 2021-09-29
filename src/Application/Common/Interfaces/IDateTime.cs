using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthorizationServer.Application.Common.Interfaces
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}