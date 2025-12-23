using MediatR;
using ErrorOr;

namespace Api.Application.Handlers.Transacao.ConsultarTotaisCategoria;

public record ConsultarTotaisCategoriaRequest() : IRequest<ErrorOr<ConsultarTotaisCategoriaResponse>>;