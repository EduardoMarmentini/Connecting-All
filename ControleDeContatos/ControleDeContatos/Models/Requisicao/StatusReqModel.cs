using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models.Requisicao
{
    public class StatusReqModel
    {
        [Key]
        public int id_status { get; set; }
        public required string descricao { get; set; }
        public required string color { get; set; }
        public required string contabiliza_horas {  get; set; } 
    }
}
