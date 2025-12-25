using Api.Application.Handlers.Pessoa.Cadastrar;
using Api.Application.Handlers.Pessoa.Listar;
using Api.Application.Handlers.Pessoa.Remover;
using ErrorOr;


using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PessoaController : ControllerBase
{
    private readonly ISender _mediator;

    public PessoaController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar(CadastrarPessoaRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);

        return result.Match(
            response => CreatedAtAction(nameof(Cadastrar), new { id = response.Id }, response),
            errors => Problem(errors.First().Description)
        );
    }

    [HttpGet]
    public async Task<IActionResult> Listar(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ListarPessoasRequest(), cancellationToken);

        return result.Match(
            pessoas => Ok(pessoas),
            errors => Problem(errors.First().Description)
        );
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Remover(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new RemoverPessoaRequest(id), ct);

        return result.Match(
            response => Ok(response),
            errors => {
                var error = errors.First();
                if (error.Type == ErrorType.NotFound)
                    return NotFound(new { erro = error.Description });
                
                return Problem(error.Description);
            }
        );
    }
}