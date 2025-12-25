using Api.Application.Errors;
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

        // Validação se a pessoa existe
        if (pessoa is null)
            return ApplicationErrors.PessoaNaoEncontrada;
        
        // Remoção da pessoa
        _repo.Remover(pessoa);
        await _repo.UnitOfWork.CommitAsync(cancellationToken);

        //Retorno do Response
        return new RemoverPessoaResponse($"Pessoa '{pessoa.Nome}' removida com sucesso!");
    }
}