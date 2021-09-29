using System;
using System.Collections.Generic;
using System.Linq;

using AuthorizationServer.Application.Common.Interfaces;

namespace AuthorizationServer.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}