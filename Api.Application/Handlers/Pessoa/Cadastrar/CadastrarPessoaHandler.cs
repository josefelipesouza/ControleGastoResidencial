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
        //Validação dos dados do request
        var validator = new CadastrarPessoaRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            return validationResult.Errors
                .Select(e => Error.Validation(e.PropertyName, e.ErrorMessage))
                .ToList();

        //Validação de Regra de Negócio
        //Verifica se a idade é valida, não pode ser menor ou igual a zero
        if (request.Idade <= 0)
        {
            return ApplicationErrors.PessoaIdadeInvalida;
        }

        //Mapeamento
        var pessoa = new Domain.Entities.Pessoa
        {
            Nome = request.Nome,
            Idade = request.Idade
        };

        //Persistência
        await _repo.AdicionarAsync(pessoa, cancellationToken);
        await _repo.UnitOfWork.CommitAsync(cancellationToken);

        //Retorno do Response
        return new CadastrarPessoaResponse(pessoa.Id, pessoa.Nome, pessoa.Idade);
    }
}