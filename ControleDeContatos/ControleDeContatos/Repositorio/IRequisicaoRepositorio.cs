using ControleDeContatos.Models.Requisicao;

namespace ControleDeContatos.Repositorio
{
    public interface IRequisicaoRepositorio
    {
        public List<RequisicaoViewModel> BuscarRequisicaoPorUsuario(int id_usuario_logado);

    }
}
