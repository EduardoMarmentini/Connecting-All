using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models.Requisicao
{
    public class RequisicaoModel
    {
        [Key]
        public int id_requisicao { get; set; }

        public required int id_usuario { get; set; }

        public required string responsavel { get; set; }

        public required string titulo_requisicao { get; set; }

        public required int status { get; set; }

        public required DateTime data_cadastro { get; set; }

        public DateTime data_entrega { get; set; }

        public DateTime? data_conclusao { get; set; }

        public DateTime horas_trabalhadas { get; set; }
    }
}
