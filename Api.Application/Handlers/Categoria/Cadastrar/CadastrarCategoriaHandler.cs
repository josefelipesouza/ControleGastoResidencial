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
        // 1. Validação Manual (seguindo seu exemplo)
        var validator = new CadastrarCategoriaRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return validationResult.Errors
                .Select(e => Error.Validation(e.PropertyName, e.ErrorMessage))
                .ToList();

        // 2. Mapeamento para Entidade
        var categoria = new Domain.Entities.Categoria
        {
            Nome = request.Nome,
            Descricao = request.Descricao,
            Finalidade = request.Finalidade
        };

        // 3. Persistência
        await _repo.AdicionarAsync(categoria, cancellationToken);
        await _repo.UnitOfWork.CommitAsync(cancellationToken);

        // 4. Retorno do Response
        return new CadastrarCategoriaResponse(
            categoria.Id, 
            categoria.Nome, 
            categoria.Descricao, 
            categoria.Finalidade);
    }
}