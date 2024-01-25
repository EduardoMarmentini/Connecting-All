using System.ComponentModel.DataAnnotations;

namespace ControleDeContatos.Models.Requisicao
{
    public class OcorrenciaReqModel
    {
        [Key]
        public int id_ocorrencia { get; set; }
        public required int id_requisicao { get; set; }
        public required string detalhe_ocorrencia { get; set; }
        public required int id_usuario { get; set; }
        public required int id_status { get; set; }
        public required DateTime data_ocorrencia { get; set; }
        public required string log_ocorrencia { get; set; }
    }
}
