using Api.Domain.Entities;
using Api.Domain.Enums;
using Api.Infrastructure.Data; // Ajuste para o seu namespace
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.Infrastructure.Seed;

public static class GastoDbSeeder
{
    public static async Task SeedAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<AppDbContext>();
        await CriarCategorias(context);
    }

    private static async Task CriarCategorias(AppDbContext context)
    {
        if (await context.Categorias.AnyAsync()) return;

        var categorias = new List<Categoria>
        {
            // RECEITAS
            new Categoria { Nome = "Salário", Finalidade = Finalidade.Receita, Descricao = "Renda mensal principal" },
            new Categoria { Nome = "Freelance", Finalidade = Finalidade.Receita, Descricao = "Renda extra" },

            // DESPESAS
            new Categoria { Nome = "Alimentação", Finalidade = Finalidade.Despesa, Descricao = "Mercado e delivery" },
            new Categoria { Nome = "Moradia", Finalidade = Finalidade.Despesa, Descricao = "Aluguel, Luz, Água" },
            new Categoria { Nome = "Saúde", Finalidade = Finalidade.Despesa, Descricao = "Farmácia e Plano" },
            
            // AMBAS
            new Categoria { Nome = "Transferência", Finalidade = Finalidade.Ambas, Descricao = "Movimentação entre contas" }
        };

        await context.Categorias.AddRangeAsync(categorias);
        await context.SaveChangesAsync();
    }
}