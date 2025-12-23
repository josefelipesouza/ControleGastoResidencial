using Api.Application.Interfaces.Data;
using Api.Application.Interfaces.Repositories;
using Api.Domain.Entities;
using Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;
    public IUnitOfWork UnitOfWork => (IUnitOfWork)_context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Categoria categoria, CancellationToken cancellationToken)
    {
        await _context.Categorias.AddAsync(categoria, cancellationToken);
    }

    public async Task<IEnumerable<Categoria>> ListarAsync(CancellationToken cancellationToken)
    {
        return await _context.Categorias.AsNoTracking().ToListAsync(cancellationToken);
    }
}