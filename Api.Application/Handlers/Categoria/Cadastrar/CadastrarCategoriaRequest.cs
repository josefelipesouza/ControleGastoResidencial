using MediatR;
using ErrorOr;
using FluentValidation;
using Api.Domain.Enums;

namespace Api.Application.Handlers.Categoria.Cadastrar;

public record CadastrarCategoriaRequest(string Nome, string Descricao, Finalidade Finalidade) 
    : IRequest<ErrorOr<CadastrarCategoriaResponse>>;

public class CadastrarCategoriaRequestValidator : AbstractValidator<CadastrarCategoriaRequest>
{
    public CadastrarCategoriaRequestValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome da categoria é obrigatório.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Descricao)
            .MaximumLength(250).WithMessage("A descrição deve ter no máximo 250 caracteres.");

        RuleFor(x => x.Finalidade)
            .IsInEnum().WithMessage("A finalidade informada é inválida.");
    }
}