using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AuthorizationServer.Domain.Common;

namespace AuthorizationServer.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}