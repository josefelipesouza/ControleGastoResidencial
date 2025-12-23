using System.Threading;
using System.Threading.Tasks;

namespace Api.Application.Interfaces.Data;

public interface IUnitOfWork
{
    // Interface não tem chaves { }, apenas o ponto e vírgula ;
    Task CommitAsync(CancellationToken cancellationToken = default);
}