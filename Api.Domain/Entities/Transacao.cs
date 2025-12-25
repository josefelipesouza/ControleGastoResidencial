namespace Api.Domain.Entities;

    public class Transacao
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public string Tipo { get; set; } = string.Empty; 

        // Relacionamento com Categoria
        public int IdCategoria { get; set; }
        public Categoria Categoria { get; set; } = null!;

        // Relacionamento com Pessoa
        public int IdPessoa { get; set; }
        public Pessoa Pessoa { get; set; } = null!;
    }
