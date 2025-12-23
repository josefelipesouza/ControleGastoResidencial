using Api.Domain.Enums;

namespace Api.Application.Handlers.Categoria.Listar;

public record ListarCategoriasResponse(
    int Id, 
    string Nome, 
    string Descricao, 
    Finalidade Finalidade);