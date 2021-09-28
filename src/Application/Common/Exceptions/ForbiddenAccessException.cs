using System;

namespace AuthorizationServer.Application.Common.Exceptions
{
    public class ForbiddenAccessException : Exception
    {
        public ForbiddenAccessException() : base() { }
    }
}
