using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public interface IUsuarioRepositorio
    {

        // Metodo para validar o acesso do usuario
        UsuarioModel BuscarPorLogin(string login);
        // Metodo para buscar o email e login do usuario
        UsuarioModel BuscarPorEmailLogin(string email, string login);
        // Metodo de buscar informaçõespor id
        UsuarioModel ListarPorId(int id);
        // Metodo de buscar todos os registros
        List<UsuarioModel> BuscarTodos();
        // Metodo de adicionar registros as tabela.
        UsuarioModel Adicionar(UsuarioModel usuario, IFormFile picture_upload);
        // Metodo para alterar os dados do usuario na tabela de usuarios
        UsuarioModel AtualizarConta(UsuarioModel usuaruo, IFormFile picture_upload);
        // Metodo para atualizar a senha do usuario
        UsuarioModel AtualizarSenha(UsuarioModel usuario);
        // Metodo para Apagar o contato na tabela de usuarios
        UsuarioModel Excluir(UsuarioModel usuario);

    }
}
