using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public interface IRequisicaoRepositorio
    {
        public List<RequisicaoModel> BuscarRequisicaoPorUsuario(int id_usuario_logado);

    }
}
