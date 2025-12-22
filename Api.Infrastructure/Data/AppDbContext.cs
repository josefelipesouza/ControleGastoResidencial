using Microsoft.EntityFrameworkCore;
using Api.Domain.Entities;

namespace Api.Infrastructure.Data;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Transacao> Transacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da Transação (Chaves Estrangeiras)
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
