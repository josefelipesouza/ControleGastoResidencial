using MediatR;
using ErrorOr;
using Api.Application.Interfaces.Repositories;
using Api.Application.Errors;
using Api.Domain.Entities;
using Api.Domain.Enums;

namespace Api.Application.Handlers.Transacao.Cadastrar;

public class CadastrarTransacaoHandler : IRequestHandler<CadastrarTransacaoRequest, ErrorOr<CadastrarTransacaoResponse>>
{
    private readonly ITransacaoRepository _transacaoRepo;
    private readonly ICategoriaRepository _categoriaRepo;
    private readonly IPessoaRepository _pessoaRepo;

    public CadastrarTransacaoHandler(
        ITransacaoRepository transacaoRepo, 
        ICategoriaRepository categoriaRepo, 
        IPessoaRepository pessoaRepo)
    {
        _transacaoRepo = transacaoRepo;
        _categoriaRepo = categoriaRepo;
        _pessoaRepo = pessoaRepo;
    }

    public async Task<ErrorOr<CadastrarTransacaoResponse>> Handle(
        CadastrarTransacaoRequest request, CancellationToken cancellationToken)
    {
        // 1. Validação do Request (FluentValidation)
        var validator = new CadastrarTransacaoRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return validationResult.Errors.Select(e => Error.Validation(e.PropertyName, e.ErrorMessage)).ToList();

        // 2. Busca de dependências
        var categoria = await _categoriaRepo.BuscarPorIdAsync(request.IdCategoria, cancellationToken);
        if (categoria is null) return ApplicationErrors.CategoriaNaoEncontrada;

        var pessoa = await _pessoaRepo.BuscarPorIdAsync(request.IdPessoa, cancellationToken);
        if (pessoa is null) return ApplicationErrors.PessoaNaoEncontrada;

        // 3. Conversão e Validação de Negócio
        // Fazemos o cast direto de int para o Enum
        var tipoEnum = (Finalidade)request.Tipo;

        // Regra: Uma transação não pode ser "Ambas" (3), apenas Receita (2) ou Despesa (1)
        if (tipoEnum == Finalidade.Ambas)
            return Error.Validation("Transacao.Tipo", "O tipo 'Ambas' é permitido apenas para Categorias.");

        // Regra: Menor de 18 anos só pode cadastrar Despesa
        if (pessoa.Idade < 18 && tipoEnum == Finalidade.Receita)
            return ApplicationErrors.ReceitaNaoPermitidaParaMenor;

        // Regra: Categoria deve ser compatível
        bool ehCompativel = categoria.Finalidade == Finalidade.Ambas || 
                            categoria.Finalidade == tipoEnum;

        if (!ehCompativel)
            return ApplicationErrors.CategoriaIncompativel;

        // 4. Mapeamento para Entidade
        var transacao = new Domain.Entities.Transacao
        {
            Descricao = request.Descricao,
            Valor = request.Valor,
            Tipo = tipoEnum.ToString(), // Se sua entidade Transacao ainda usa string no banco
            IdCategoria = request.IdCategoria,
            IdPessoa = request.IdPessoa
        };

        // 5. Persistência
        await _transacaoRepo.AdicionarAsync(transacao, cancellationToken);
        await _transacaoRepo.UnitOfWork.CommitAsync(cancellationToken);

        // 6. Retorno
        return new CadastrarTransacaoResponse(
            transacao.Id,
            transacao.Descricao,
            transacao.Valor,
            transacao.Tipo,
            transacao.IdCategoria,
            transacao.IdPessoa);
    }
}