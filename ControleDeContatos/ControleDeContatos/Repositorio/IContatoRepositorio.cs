using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public interface IContatoRepositorio
    {   
        // Metodo de buscar informaçõespor id
        ContatoModel ListarPorId(int id);
        // Metodo de buscar todos os registros
        List<ContatoModel> BuscarTodos();
        // Metodo de adicionar registros as tabela.
        ContatoModel Adicionar(ContatoModel contato, IFormFile picture_upload);

        ContatoModel Alterar(ContatoModel contato, IFormFile picture_upload, IFormFile foto_perfil);
    }
}