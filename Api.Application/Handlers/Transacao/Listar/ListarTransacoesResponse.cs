namespace Api.Application.Handlers.Transacao.Listar;

public record ListarTransacoesResponse(
    int Id,
    string Descricao,
    decimal Valor,
    string Tipo,
    string NomeCategoria,
    string NomePessoa);