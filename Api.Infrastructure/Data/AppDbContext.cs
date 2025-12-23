using Microsoft.EntityFrameworkCore;
using Api.Domain.Entities;
using Api.Application.Interfaces.Data;

namespace Api.Infrastructure.Data;

public class AppDbContext : DbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }

    // --- ADICIONE ESTE MÉTODO AQUI ---
    // Remova o <bool> para ficar igual à interface
    public async Task CommitAsync(CancellationToken ct = default)
    {
        // Apenas aguarda o salvamento. 
        // O Entity Framework jogará uma exceção se algo der errado, 
        // o que já é suficiente para o MediatR interromper o fluxo.
        await base.SaveChangesAsync(ct);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Transacao>()
            .HasOne(t => t.Pessoa)
            .WithMany(p => p.Transacoes)
            .HasForeignKey(t => t.IdPessoa);

        modelBuilder.Entity<Transacao>()
            .HasOne(t => t.Categoria)
            .WithMany()
            .HasForeignKey(t => t.IdCategoria);
    }
}