using System.Threading;
using System.Threading.Tasks;

namespace Api.Application.Interfaces.Data;

public interface IUnitOfWork
{
    Task CommitAsync(CancellationToken cancellationToken = default);
}