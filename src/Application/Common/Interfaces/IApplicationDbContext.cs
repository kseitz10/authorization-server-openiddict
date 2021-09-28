using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace AuthorizationServer.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
