using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public class RequisicaoRepositorio : IRequisicaoRepositorio
    {
        private readonly BancoContext _bancoContext;

        public RequisicaoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }

        List<RequisicaoModel> IRequisicaoRepositorio.BuscarRequisicaoPorUsuario(int id_usuario_logado)
        {
            return _bancoContext.requisicoes.Where(e => e.id_usuario == id_usuario_logado).ToList();
        }
    }
}
