using AuthorizationServer.Domain.Common;
using System.Threading.Tasks;

namespace AuthorizationServer.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
