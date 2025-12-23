namespace Api.Application.Handlers.Transacao.ConsultarTotaisCategoria;

public record ConsultarTotaisCategoriaResponse(
    List<ItemTotalCategoria> Itens,
    decimal TotalGeralReceitas,
    decimal TotalGeralDespesas,
    decimal SaldoGeralLiquido);

public record ItemTotalCategoria(
    int CategoriaId,
    string NomeCategoria,
    decimal TotalReceitas,
    decimal TotalDespesas,
    decimal Saldo);