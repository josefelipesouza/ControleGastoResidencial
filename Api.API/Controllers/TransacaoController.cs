using Api.Application.Handlers.Transacao.Cadastrar;
using Api.Application.Handlers.Transacao.ConsultarTotais;
using Api.Application.Handlers.Transacao.Listar;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransacaoController : ControllerBase
{
    private readonly ISender _mediator;

    public TransacaoController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar(
        [FromBody] CadastrarTransacaoRequest request, 
        CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);

        return result.Match(
            response => CreatedAtAction(nameof(Cadastrar), new { id = response.Id }, response),
            errors => {
                // Se for erro de validação (FluentValidation), retornamos 400 Bad Request
                if (errors.Any(e => e.Type == ErrorOr.ErrorType.Validation))
                    return BadRequest(errors);

                // Caso contrário, retornamos o primeiro erro como um problema de negócio
                return Problem(
                    detail: errors.First().Description, 
                    statusCode: StatusCodes.Status400BadRequest
                );
            }
        );
    }

    [HttpGet]
    public async Task<IActionResult> Listar(CancellationToken ct)
    {
        var result = await _mediator.Send(new ListarTransacoesRequest(), ct);

        return result.Match(
            transacoes => Ok(transacoes),
            errors => Problem(errors.First().Description)
        );
    }

    [HttpGet("totais-por-pessoa")]
    public async Task<IActionResult> ConsultarTotais(CancellationToken ct)
    {
        var result = await _mediator.Send(new ConsultarTotaisPessoaRequest(), ct);

        return result.Match(
            response => Ok(response),
            errors => Problem(errors.First().Description)
        );
    }
}