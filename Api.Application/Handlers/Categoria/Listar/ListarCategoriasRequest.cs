using ErrorOr;
using MediatR;

namespace Api.Application.Handlers.Categoria.Listar;

public record ListarCategoriasRequest() : IRequest<ErrorOr<IEnumerable<ListarCategoriasResponse>>>;