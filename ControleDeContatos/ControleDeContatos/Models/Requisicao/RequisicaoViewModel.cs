using ControleDeContatos.Models.Cliente;
using ControleDeContatos.Models.Usuario;

namespace ControleDeContatos.Models.Requisicao
{
    public class RequisicaoViewModel
    {

        public RequisicaoModel requisicao { get; set; }
        public CriarReqModel criar_req { get; set; }
        public StatusReqModel statusReq { get; set; }
        public ContatoModel cliente { get; set; }
        public UsuarioModel usuario { get; set; }
        public PrioridadeModel prioridade { get; set; }
        public RequisicaoOcorrenciaModel requisicao_ocorrencia { get; set; }
    }
}
