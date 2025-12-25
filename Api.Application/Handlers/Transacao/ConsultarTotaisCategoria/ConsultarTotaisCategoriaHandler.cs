using MediatR;
using ErrorOr;
using Api.Application.Interfaces.Repositories;

namespace Api.Application.Handlers.Transacao.ConsultarTotaisCategoria;

public class ConsultarTotaisCategoriaHandler : IRequestHandler<ConsultarTotaisCategoriaRequest, ErrorOr<ConsultarTotaisCategoriaResponse>>
{
    private readonly ICategoriaRepository _categoriaRepo;
    private readonly ITransacaoRepository _transacaoRepo;

    public ConsultarTotaisCategoriaHandler(ICategoriaRepository categoriaRepo, ITransacaoRepository transacaoRepo)
    {
        _categoriaRepo = categoriaRepo;
        _transacaoRepo = transacaoRepo;
    }

    public async Task<ErrorOr<ConsultarTotaisCategoriaResponse>> Handle(ConsultarTotaisCategoriaRequest request, CancellationToken ct)
    {
        var categorias = await _categoriaRepo.ListarAsync(ct);
        var transacoes = await _transacaoRepo.ListarAsync(ct);

        var itens = new List<ItemTotalCategoria>();

        //Agrupa as transações por categoria e calcula os totais por tipo de transação
        foreach (var categoria in categorias)
        {
            var transacoesCategoria = transacoes.Where(t => t.IdCategoria == categoria.Id).ToList();

            decimal receitas = transacoesCategoria
                .Where(t => t.Tipo == "Receita" || t.Tipo == "2")
                .Sum(t => t.Valor);

            decimal despesas = transacoesCategoria
                .Where(t => t.Tipo == "Despesa" || t.Tipo == "1")
                .Sum(t => t.Valor);

            itens.Add(new ItemTotalCategoria(
                categoria.Id,
                categoria.Nome,
                receitas,
                despesas,
                receitas - despesas
            ));
        }

        //Cálculo dos totais gerais por Receita e Despesa
        var totalGeralReceitas = itens.Sum(i => i.TotalReceitas);
        var totalGeralDespesas = itens.Sum(i => i.TotalDespesas);
        
        //Retorno do Response
        return new ConsultarTotaisCategoriaResponse(
            itens,
            totalGeralReceitas,
            totalGeralDespesas,
            totalGeralReceitas - totalGeralDespesas
        );
    }
}