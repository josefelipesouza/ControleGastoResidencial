using MediatR;
using ErrorOr;

namespace Api.Application.Handlers.Transacao.Listar;

// Request (pode ser vazio ou conter filtros como IdPessoa futuramente)
public record ListarTransacoesRequest() : IRequest<ErrorOr<List<ListarTransacoesResponse>>>;
