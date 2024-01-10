using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models.Requisicao
{
    public class PrioridadeModel
    {
        [Key]
        public int id_prioridade { get; set; }

        public required string descricao { get; set; }

        public required string color { get; set; }
    }
}
