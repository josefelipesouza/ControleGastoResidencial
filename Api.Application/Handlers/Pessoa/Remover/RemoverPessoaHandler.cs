using Api.Application.Interfaces.Repositories;
using ErrorOr;
using MediatR;

namespace Api.Application.Handlers.Pessoa.Remover;

public class RemoverPessoaHandler : IRequestHandler<RemoverPessoaRequest, ErrorOr<RemoverPessoaResponse>>
{
    private readonly IPessoaRepository _repo;

    public RemoverPessoaHandler(IPessoaRepository repo) => _repo = repo;

    public async Task<ErrorOr<RemoverPessoaResponse>> Handle(RemoverPessoaRequest request, CancellationToken cancellationToken)
    {
        var pessoa = await _repo.BuscarPorIdAsync(request.Id, cancellationToken);

        if (pessoa is null)
            return Error.NotFound(description: $"Pessoa com ID {request.Id} não encontrada.");

        _repo.Remover(pessoa);
        await _repo.UnitOfWork.CommitAsync(cancellationToken);

        return new RemoverPessoaResponse($"Pessoa '{pessoa.Nome}' removida com sucesso!");
    }
}