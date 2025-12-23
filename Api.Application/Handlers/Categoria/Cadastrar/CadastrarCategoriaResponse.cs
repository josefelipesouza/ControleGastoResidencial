using Api.Domain.Enums;

namespace Api.Application.Handlers.Categoria.Cadastrar;

public record CadastrarCategoriaResponse(int Id, string Nome, string Descricao, Finalidade Finalidade);