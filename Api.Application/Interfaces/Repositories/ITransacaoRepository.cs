using Api.Application.Interfaces.Data;
using Api.Domain.Entities;

namespace Api.Application.Interfaces.Repositories;

public interface ITransacaoRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task AdicionarAsync(Transacao transacao, CancellationToken cancellationToken);
    Task<IEnumerable<Transacao>> ListarAsync(CancellationToken cancellationToken);
}