using MediatR;
using ErrorOr;
using Api.Application.Interfaces.Repositories;
using Api.Domain.Entities;

namespace Api.Application.Handlers.Categoria.Cadastrar;

public class CadastrarCategoriaHandler : IRequestHandler<CadastrarCategoriaRequest, ErrorOr<CadastrarCategoriaResponse>>
{
    private readonly ICategoriaRepository _repo;

    public CadastrarCategoriaHandler(ICategoriaRepository repo)
    {
        _repo = repo;
    }

    public async Task<ErrorOr<CadastrarCategoriaResponse>> Handle(
        CadastrarCategoriaRequest request, CancellationToken cancellationToken)
    {
        //Validação dados do request
        var validator = new CadastrarCategoriaRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return validationResult.Errors
                .Select(e => Error.Validation(e.PropertyName, e.ErrorMessage))
                .ToList();

        //Mapeamento para Entidade
        var categoria = new Domain.Entities.Categoria
        {
            Nome = request.Nome,
            Descricao = request.Descricao,
            Finalidade = request.Finalidade
        };

        //Persistência
        await _repo.AdicionarAsync(categoria, cancellationToken);
        await _repo.UnitOfWork.CommitAsync(cancellationToken);

        //Retorno do Response
        return new CadastrarCategoriaResponse(
            categoria.Id, 
            categoria.Nome, 
            categoria.Descricao, 
            categoria.Finalidade);
    }
}