using MediatR;
using ErrorOr;
using Api.Application.Interfaces.Repositories;

namespace Api.Application.Handlers.Transacao.Listar;

public class ListarTransacoesHandler : IRequestHandler<ListarTransacoesRequest, ErrorOr<List<ListarTransacoesResponse>>>
{
    private readonly ITransacaoRepository _transacaoRepo;
    private readonly ICategoriaRepository _categoriaRepo;
    private readonly IPessoaRepository _pessoaRepo;

    public ListarTransacoesHandler(
        ITransacaoRepository transacaoRepo,
        ICategoriaRepository categoriaRepo,
        IPessoaRepository pessoaRepo)
    {
        _transacaoRepo = transacaoRepo;
        _categoriaRepo = categoriaRepo;
        _pessoaRepo = pessoaRepo;
    }

    public async Task<ErrorOr<List<ListarTransacoesResponse>>> Handle(
        ListarTransacoesRequest request, CancellationToken cancellationToken)
    {
        var transacoes = await _transacaoRepo.ListarAsync(cancellationToken);

        var response = new List<ListarTransacoesResponse>();

        foreach (var t in transacoes)
        {
            // Busca os nomes para o retorno (em um cenário real, você usaria um Include no EF)
            var categoria = await _categoriaRepo.BuscarPorIdAsync(t.IdCategoria, cancellationToken);
            var pessoa = await _pessoaRepo.BuscarPorIdAsync(t.IdPessoa, cancellationToken);

            response.Add(new ListarTransacoesResponse(
                t.Id,
                t.Descricao,
                t.Valor,
                t.Tipo,
                categoria?.Nome ?? "N/A",
                pessoa?.Nome ?? "N/A"
            ));
        }

        return response;
    }
}