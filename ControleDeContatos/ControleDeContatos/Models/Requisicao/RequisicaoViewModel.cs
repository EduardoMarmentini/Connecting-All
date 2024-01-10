using ControleDeContatos.Models.Cliente;

namespace ControleDeContatos.Models.Requisicao
{
    public class RequisicaoViewModel
    {

        public RequisicaoModel requisicao { get; set; }

        public StatusReqModel statusReq { get; set; }
    
        public ContatoModel cliente { get; set; }

        public PrioridadeModel prioridade { get; set; }
    }
}
