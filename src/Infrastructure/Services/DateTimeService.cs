using AuthorizationServer.Application.Common.Interfaces;
using System;

namespace AuthorizationServer.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
