using MediatR;
using ErrorOr;
using FluentValidation;

namespace Api.Application.Handlers.Pessoa.Cadastrar;

public record CadastrarPessoaRequest(string Nome, int Idade) 
    : IRequest<ErrorOr<CadastrarPessoaResponse>>;

public class CadastrarPessoaRequestValidator : AbstractValidator<CadastrarPessoaRequest>
{
    public CadastrarPessoaRequestValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome é obrigatório.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Idade)
            .NotEmpty().WithMessage("A idade é obrigatória.");
    }
}