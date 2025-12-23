using Api.Application.Interfaces.Data;
using Api.Domain.Entities;

namespace Api.Application.Interfaces.Repositories;

public interface IPessoaRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task AdicionarAsync(Pessoa pessoa, CancellationToken cancellationToken);
    Task<IEnumerable<Pessoa>> ListarAsync(CancellationToken cancellationToken);
    Task<Pessoa?> BuscarPorIdAsync(int id, CancellationToken cancellationToken);
    void Remover(Pessoa pessoa);
}