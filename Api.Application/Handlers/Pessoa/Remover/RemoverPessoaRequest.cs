using ErrorOr;
using MediatR;

namespace Api.Application.Handlers.Pessoa.Remover;

public record RemoverPessoaRequest(int Id) : IRequest<ErrorOr<RemoverPessoaResponse>>;