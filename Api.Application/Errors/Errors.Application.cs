using ErrorOr;

namespace Api.Application.Errors;

public static class ApplicationErrors
{
    public static readonly Error PessoaNaoEncontrada = 
        Error.NotFound(code: "Pessoa.NaoEncontrada", description: "Pessoa não encontrada.");

    public static readonly Error CategoriaNaoEncontrada = 
        Error.NotFound(code: "Categoria.NaoEncontrada", description: "Categoria não encontrada.");
        
    public static readonly Error TransacaoNaoEncontrada = 
        Error.NotFound(code: "Transacao.NaoEncontrada", description: "Transação não encontrada.");
        
    public static readonly Error ReceitaNaoPermitidaParaMenor = 
        Error.Validation(code: "Transacao.ReceitaNaoPermitida", description: "Não é permitido Lançamento de Receita para menor de 18 anos.");

    public static readonly Error CategoriaIncompativel = 
        Error.Validation(code: "Transacao.CategoriaIncompativel", description: "A categoria deve ser do mesmo tipo da transação.");    
}