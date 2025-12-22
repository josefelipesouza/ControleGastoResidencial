namespace Api.Domain.Entities;

    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Idade { get; set; }
        
        // Propriedade de navegação para as transações desta pessoa
        public ICollection<Transacao> Transacoes { get; set; } = new List<Transacao>();
    }
