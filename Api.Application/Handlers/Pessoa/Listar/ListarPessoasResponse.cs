namespace Api.Application.Handlers.Pessoa.Listar;

public record ListarPessoasResponse(
    int Id, 
    string Nome, 
    int Idade);