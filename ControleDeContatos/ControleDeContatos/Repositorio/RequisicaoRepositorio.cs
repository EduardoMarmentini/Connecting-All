using ControleDeContatos.Data;
using ControleDeContatos.Models.Requisicao;

namespace ControleDeContatos.Repositorio
{
    public class RequisicaoRepositorio : IRequisicaoRepositorio
    {
        private readonly BancoContext _bancoContext;

        public RequisicaoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        List<RequisicaoViewModel> IRequisicaoRepositorio.BuscarRequisicaoPorUsuario(int id_usuario_logado)
        {
            return _bancoContext.requisicoes
            .Where(e => e.id_usuario == id_usuario_logado)
            .Join(
                _bancoContext.status_requisicao,
                requisicao => requisicao.status,
                status => status.id_status, 
                (requisicao, status) => new RequisicaoViewModel
                {
                    requisicao = requisicao,
                    statusReq = status
                })
            .ToList();
        }
    }
}
