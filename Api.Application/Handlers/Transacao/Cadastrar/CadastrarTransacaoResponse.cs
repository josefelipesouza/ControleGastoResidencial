namespace Api.Application.Handlers.Transacao.Cadastrar;

public record CadastrarTransacaoResponse(
    int Id, 
    string Descricao, 
    decimal Valor, 
    string Tipo, 
    int IdCategoria, 
    int IdPessoa);