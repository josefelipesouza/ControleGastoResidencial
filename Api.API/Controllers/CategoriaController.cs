using Api.Application.Handlers.Categoria.Cadastrar;
using Api.Application.Handlers.Categoria.Listar;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ISender _mediator;

    public CategoriaController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> Listar(CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new ListarCategoriasRequest(), cancellationToken);

        return result.Match(
            categorias => Ok(categorias),
            errors => Problem(errors.First().Description)
        );
    }

    [HttpPost]
    public async Task<IActionResult> Cadastrar(CadastrarCategoriaRequest request, CancellationToken ct)
    {
        var result = await _mediator.Send(request, ct);

        return result.Match(
            response => CreatedAtAction(nameof(Cadastrar), new { id = response.Id }, response),
            errors => Problem(errors.First().Description)
        );
    }
}