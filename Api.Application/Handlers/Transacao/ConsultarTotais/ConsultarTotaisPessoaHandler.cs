using MediatR;
using ErrorOr;
using Api.Application.Interfaces.Repositories;
using Api.Domain.Enums;

namespace Api.Application.Handlers.Transacao.ConsultarTotais;

public class ConsultarTotaisPessoaHandler : IRequestHandler<ConsultarTotaisPessoaRequest, ErrorOr<ConsultarTotaisPessoaResponse>>
{
    private readonly IPessoaRepository _pessoaRepo;
    private readonly ITransacaoRepository _transacaoRepo;

    public ConsultarTotaisPessoaHandler(IPessoaRepository pessoaRepo, ITransacaoRepository transacaoRepo)
    {
        _pessoaRepo = pessoaRepo;
        _transacaoRepo = transacaoRepo;
    }

    public async Task<ErrorOr<ConsultarTotaisPessoaResponse>> Handle(ConsultarTotaisPessoaRequest request, CancellationToken ct)
    {
        var pessoas = await _pessoaRepo.ListarAsync(ct);
        var transacoes = await _transacaoRepo.ListarAsync(ct);

        var itens = new List<ItemTotalPessoa>();

        foreach (var pessoa in pessoas)
        {
            var transacoesPessoa = transacoes.Where(t => t.IdPessoa == pessoa.Id).ToList();

            // Soma baseada no texto ou enum (ajuste conforme seu banco salva o 'Tipo')
            decimal receitas = transacoesPessoa
                .Where(t => t.Tipo == "Receita" || t.Tipo == "2")
                .Sum(t => t.Valor);

            decimal despesas = transacoesPessoa
                .Where(t => t.Tipo == "Despesa" || t.Tipo == "1")
                .Sum(t => t.Valor);

            itens.Add(new ItemTotalPessoa(
                pessoa.Id,
                pessoa.Nome,
                receitas,
                despesas,
                receitas - despesas
            ));
        }

        var totalGeralReceitas = itens.Sum(i => i.TotalReceitas);
        var totalGeralDespesas = itens.Sum(i => i.TotalDespesas);

        return new ConsultarTotaisPessoaResponse(
            itens,
            totalGeralReceitas,
            totalGeralDespesas,
            totalGeralReceitas - totalGeralDespesas
        );
    }
}