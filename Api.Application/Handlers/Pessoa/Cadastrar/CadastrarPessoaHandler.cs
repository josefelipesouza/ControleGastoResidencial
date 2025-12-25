using MediatR;
using ErrorOr;
using Api.Application.Interfaces.Repositories;
using Api.Application.Errors;
using Api.Domain.Entities;

namespace Api.Application.Handlers.Pessoa.Cadastrar;

public class CadastrarPessoaHandler : IRequestHandler<CadastrarPessoaRequest, ErrorOr<CadastrarPessoaResponse>>
{
    private readonly IPessoaRepository _repo;

    public CadastrarPessoaHandler(IPessoaRepository repo)
    {
        _repo = repo;
    }

    public async Task<ErrorOr<CadastrarPessoaResponse>> Handle(
        CadastrarPessoaRequest request, CancellationToken cancellationToken)
    {
        // 1. Validação de Sintaxe (FluentValidation)
        var validator = new CadastrarPessoaRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return validationResult.Errors
                .Select(e => Error.Validation(e.PropertyName, e.ErrorMessage))
                .ToList();

        // 2. Validação de Regra de Negócio
        // Verifica se a idade é zero ou negativa conforme sua solicitação
        if (request.Idade <= 0)
        {
            return ApplicationErrors.PessoaIdadeInvalida;
        }

        // 3. Mapeamento
        var pessoa = new Domain.Entities.Pessoa
        {
            Nome = request.Nome,
            Idade = request.Idade
        };

        // 4. Persistência
        await _repo.AdicionarAsync(pessoa, cancellationToken);
        await _repo.UnitOfWork.CommitAsync(cancellationToken);

        // 5. Resposta
        return new CadastrarPessoaResponse(pessoa.Id, pessoa.Nome, pessoa.Idade);
    }
}