using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public interface IUsuarioRepositorio
    {

        //Metodo para validar o acesso do usuario
        UsuarioModel BuscarPorLogin(string login);
        // Metodo de buscar informaçõespor id
        UsuarioModel ListarPorId(int id);
        // Metodo de buscar todos os registros
        List<UsuarioModel> BuscarTodos();
        // Metodo de adicionar registros as tabela.
        UsuarioModel Adicionar(UsuarioModel usuario, IFormFile picture_upload);
        // Metodo para alterar os dados do contato na tabela de usuarios
        UsuarioModel Alterar(UsuarioModel usuaruo, IFormFile picture_upload);
        // Metodo para Apagar o contato na tabela de usuarios
        UsuarioModel Excluir(UsuarioModel usuario);

    }
}
