using ControleDeContatos.Models.Requisicao;

namespace ControleDeContatos.Repositorio
{
    public interface IRequisicaoRepositorio
    {
        public List<RequisicaoViewModel> BuscarRequisicaoPorUsuario(int id_usuario_logado);

        public List<RequisicaoViewModel> BuscarSugestao(string txtSugestao, string tipo_sujestao);

    }
}
