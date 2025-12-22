using Api.Domain.Enums;

namespace Api.Domain.Entities;

    public class Categoria
    {
        public int Id { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public Finalidade Finalidade { get; set; }
    }
