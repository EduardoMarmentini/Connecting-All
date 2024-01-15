using ControleDeContatos.Models.Requisicao;
using ControleDeContatos.Models.Usuario;

namespace ControleDeContatos.Repositorio
{
    public interface IRequisicaoRepositorio
    {
        public List<RequisicaoViewModel> BuscarRequisicaoPorUsuario(int id_usuario_logado);

        public List<UsuarioModel> BuscaResponsavelPorNome(string txtSugestao);

    }
}
