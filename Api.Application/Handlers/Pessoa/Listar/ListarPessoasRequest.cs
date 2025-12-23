using ErrorOr;
using MediatR;

namespace Api.Application.Handlers.Pessoa.Listar;

public record ListarPessoasRequest() : IRequest<ErrorOr<IEnumerable<ListarPessoasResponse>>>;