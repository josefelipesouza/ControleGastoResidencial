using MediatR;
using ErrorOr;
using FluentValidation;
using Api.Domain.Enums;

namespace Api.Application.Handlers.Transacao.Cadastrar;

public record CadastrarTransacaoRequest(
    string Descricao, 
    decimal Valor, 
    Finalidade Tipo, 
    int IdCategoria, 
    int IdPessoa) : IRequest<ErrorOr<CadastrarTransacaoResponse>>;

// Classe de Validação usando FluentValidation
public class CadastrarTransacaoRequestValidator : AbstractValidator<CadastrarTransacaoRequest>
{
    public CadastrarTransacaoRequestValidator()
    {
        RuleFor(x => x.Descricao).NotEmpty().MaximumLength(200);
        RuleFor(x => x.Valor).GreaterThan(0).WithMessage("O valor deve ser maior que zero.");
        
        RuleFor(x => x.Tipo)
            .IsInEnum<CadastrarTransacaoRequest, Finalidade>() 
            .WithMessage("O tipo de transação deve ser 1 (Despesa) ou 2 (Receita).");

        RuleFor(x => x.IdCategoria).NotEmpty();
        RuleFor(x => x.IdPessoa).NotEmpty();
    }
}