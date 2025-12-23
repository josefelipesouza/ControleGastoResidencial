namespace Api.Application.Handlers.Transacao.ConsultarTotais;

public record ConsultarTotaisPessoaResponse(
    List<ItemTotalPessoa> Itens,
    decimal TotalGeralReceitas,
    decimal TotalGeralDespesas,
    decimal SaldoGeralLiquido);

public record ItemTotalPessoa(
    int PessoaId,
    string NomePessoa,
    decimal TotalReceitas,
    decimal TotalDespesas,
    decimal Saldo);