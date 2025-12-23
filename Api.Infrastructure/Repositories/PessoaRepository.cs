using Api.Application.Interfaces.Data;
using Api.Application.Interfaces.Repositories;
using Api.Domain.Entities;
using Api.Infrastructure.Data; // Seu AppDbContext
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Repositories;

public class PessoaRepository : IPessoaRepository
{
    private readonly AppDbContext _context;
    public IUnitOfWork UnitOfWork => (IUnitOfWork)_context;

    public PessoaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Pessoa pessoa, CancellationToken cancellationToken)
    {
        await _context.Pessoas.AddAsync(pessoa, cancellationToken);
    }

    public async Task<IEnumerable<Pessoa>> ListarAsync(CancellationToken cancellationToken)
    {
        return await _context.Pessoas.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Pessoa?> BuscarPorIdAsync(int id, CancellationToken cancellationToken)
    {
        return await _context.Pessoas.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public void Remover(Pessoa pessoa)
    {
        _context.Pessoas.Remove(pessoa);
    }
}