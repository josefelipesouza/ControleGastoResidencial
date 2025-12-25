using Api.Application.Interfaces.Data;
using Api.Application.Interfaces.Repositories;
using Api.Domain.Entities;
using Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Infrastructure.Repositories;

//classe de repositório para Transacao implementando ITransacaoRepository
public class TransacaoRepository : ITransacaoRepository
{
    private readonly AppDbContext _context;
    public IUnitOfWork UnitOfWork => (IUnitOfWork)_context;

    public TransacaoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Transacao transacao, CancellationToken cancellationToken)
    {
        await _context.Transacoes.AddAsync(transacao, cancellationToken);
    }

    public async Task<IEnumerable<Transacao>> ListarAsync(CancellationToken cancellationToken)
    {
        // Propriedades de navegação para  Pessoa e Categoria
        return await _context.Transacoes
            .Include(t => t.Categoria)
            .Include(t => t.Pessoa)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }
}