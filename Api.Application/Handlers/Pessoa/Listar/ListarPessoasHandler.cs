using Api.Application.Interfaces.Repositories;
using ErrorOr;
using MediatR;

namespace Api.Application.Handlers.Pessoa.Listar;

public class ListarPessoasHandler : 
    IRequestHandler<ListarPessoasRequest, ErrorOr<IEnumerable<ListarPessoasResponse>>>
{
    private readonly IPessoaRepository _repo;

    public ListarPessoasHandler(IPessoaRepository repo)
    {
        _repo = repo;
    }

    //Monta lista de pessoas
    public async Task<ErrorOr<IEnumerable<ListarPessoasResponse>>> Handle(
        ListarPessoasRequest request, CancellationToken cancellationToken)
    {
        var pessoas = await _repo.ListarAsync(cancellationToken);

        var response = pessoas.Select(p => 
            new ListarPessoasResponse(
                p.Id,
                p.Nome,
                p.Idade
            )).ToList();

        //Retorna a lista de pessoas
        return response;
    }
}