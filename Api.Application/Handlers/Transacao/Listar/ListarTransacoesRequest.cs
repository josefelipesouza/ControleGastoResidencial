using MediatR;
using ErrorOr;

namespace Api.Application.Handlers.Transacao.Listar;

public record ListarTransacoesRequest() : IRequest<ErrorOr<List<ListarTransacoesResponse>>>;
