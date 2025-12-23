using MediatR;
using ErrorOr;

namespace Api.Application.Handlers.Transacao.ConsultarTotais;

public record ConsultarTotaisPessoaRequest() : IRequest<ErrorOr<ConsultarTotaisPessoaResponse>>;