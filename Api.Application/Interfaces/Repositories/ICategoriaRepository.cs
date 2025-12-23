using Api.Application.Interfaces.Data;
using Api.Domain.Entities;

namespace Api.Application.Interfaces.Repositories;

public interface ICategoriaRepository
{
    IUnitOfWork UnitOfWork { get; }
    Task AdicionarAsync(Categoria categoria, CancellationToken cancellationToken);
    Task<IEnumerable<Categoria>> ListarAsync(CancellationToken cancellationToken);
    Task<Categoria?> BuscarPorIdAsync(int id, CancellationToken ct);
}