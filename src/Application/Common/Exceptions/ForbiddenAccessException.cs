using System;
using System.Collections.Generic;
using System.Linq;

namespace AuthorizationServer.Application.Common.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException() : base()
        {
        }
    }
}