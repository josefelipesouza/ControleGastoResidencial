using Api.Application.Interfaces.Repositories;
using ErrorOr;
using MediatR;

namespace Api.Application.Handlers.Categoria.Listar;

public class ListarCategoriasHandler : 
    IRequestHandler<ListarCategoriasRequest, ErrorOr<IEnumerable<ListarCategoriasResponse>>>
{
    private readonly ICategoriaRepository _repo;

    public ListarCategoriasHandler(ICategoriaRepository repo)
    {
        _repo = repo;
    }

    //Monta lista de categorias
    public async Task<ErrorOr<IEnumerable<ListarCategoriasResponse>>> Handle(
        ListarCategoriasRequest request, CancellationToken cancellationToken)
    {
        var categorias = await _repo.ListarAsync(cancellationToken);

        var response = categorias.Select(c => 
            new ListarCategoriasResponse(
                c.Id,
                c.Nome,
                c.Descricao,
                c.Finalidade
            )).ToList();
            
        //Retorna a lista de categorias
        return response;
    }
}