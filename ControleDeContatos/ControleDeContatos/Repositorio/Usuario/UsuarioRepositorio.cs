using ControleDeContatos.Data;
using ControleDeContatos.Models.Usuario;
using ControleDeContatos.Repositorio.Photo;

namespace ControleDeContatos.Repositorio.Usuario
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        // Criando a varivel que ira receber as propriedades do contexto de banco
        private readonly BancoContext _bancoContext;
        private readonly IPhotoRepositorio _photo;
        // Variavel que contem o tipo da controller para especificar a pagina que as fotos seram salvas
        private string TypeController = "Usuarios";

        public UsuarioRepositorio(BancoContext bancoContext, IPhotoRepositorio photo)
        {
            // Instanciando as dependecias do banco no caso a String de conexão
            _bancoContext = bancoContext;
            _photo = photo;
        }

        // Metodo que busca o usuario pelo login 
        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }
        public UsuarioModel BuscarPorEmailLogin(string email, string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper() && x.Email.ToUpper() == email.ToUpper());
        }

        // Busca todos os dados referentes ao id passado.
        public UsuarioModel ListarPorId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        // Metodo que lista todos os dados da tabela
        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuarios.ToList();
        }

        public UsuarioModel Adicionar(UsuarioModel usuario, IFormFile picture_upload)
        {
            // Seta a data do cadastro como a data de hoje
            usuario.DataCadastro = DateTime.Now;
            // Seta o hash de cripitografia para a senha
            usuario.SetSenhaHash();
            // Chama o metodo de gravar e seleciona a tabela desejada como .Contatos
            _bancoContext.Usuarios.Add(usuario);

            // Realiza o commmit no banco de dados
            _bancoContext.SaveChanges();

            // Salva a foto de perfil do usuario
            _photo.UploadPhoto(usuario.Id, picture_upload, TypeController);

            return usuario;
        }

        public UsuarioModel AtualizarConta(EditarContaModel usuario, IFormFile picture_upload)
        {
            UsuarioModel usuarioDB = ListarPorId(usuario.Id);

            if (usuarioDB == null) throw new Exception("Houve um erro na alteração do usuario");

            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Email = usuario.Email;
            usuarioDB.Login = usuario.Login;
            // Caso o administrador não tenha informado uma senha ele não altera e mantem a ja presente
            if (usuario.New_Password != null)
            {
                usuarioDB.Password = usuario.New_Password;
                // Cripitografa a nova senha.
                usuarioDB.SetSenhaHash();
            }
            usuarioDB.DataAlteração = DateTime.Now;

            // Chama o metodo de atualizar os dados do banco do entityFrameworkCore 
            _bancoContext.Usuarios.Update(usuarioDB);

            // Chama o metodo de atualizar a foto de perfil
            _photo.AlterarPhoto(usuario.Id, picture_upload, TypeController);
            // Realiza o commit no banco de dados
            _bancoContext.SaveChanges();

            return usuarioDB;

        }

        public UsuarioModel AtualizarUsuario(UsuarioModel usuario, IFormFile picture_upload)
        {
            UsuarioModel usuarioDB = ListarPorId(usuario.Id);

            if (usuarioDB == null) throw new Exception("Houve um erro na alteração do usuario");

            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Email = usuario.Email;
            usuarioDB.Login = usuario.Login;
            // Caso o administrador não tenha informado uma senha ele não altera e mantem a ja presente
            if (usuario.Password != null)
            {
                usuarioDB.Password = usuario.Password;
                // Cripitografa a nova senha.
                usuarioDB.SetSenhaHash();
            }
            usuarioDB.DataAlteração = DateTime.Now;

            // Chama o metodo de atualizar os dados do banco do entityFrameworkCore 
            _bancoContext.Usuarios.Update(usuarioDB);

            // Chama o metodo de atualizar a foto de perfil
            _photo.AlterarPhoto(usuario.Id, picture_upload, TypeController);
            // Realiza o commit no banco de dados
            _bancoContext.SaveChanges();

            return usuarioDB;

        }
        public UsuarioModel AtualizarSenha(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = ListarPorId(usuario.Id);

            if (usuarioDB == null) throw new Exception("Houve um erro ao tentar salvar a nova senha do usuário");

            // Seta a nova senha do usuario para acessar sua conta 
            usuarioDB.Password = usuario.Password;
            usuarioDB.SetSenhaHash();
            usuarioDB.DataAlteração = DateTime.Now;

            // Chama o metodo de atualizar os dados do banco do entityFrameworkCore 
            _bancoContext.Usuarios.Update(usuarioDB);

            // Realiza o commit no banco de dados
            _bancoContext.SaveChanges();

            return usuarioDB;
        }

        public UsuarioModel Excluir(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = ListarPorId(usuario.Id);

            if (usuarioDB == null) throw new Exception("Houve um erro na exclusão do contato");

            // Chama a função que remove os dados do banco de dados
            _bancoContext.Usuarios.Remove(usuarioDB);

            // Chama o metodo que exclui a foto de perfil
            _photo.ExcluirPhoto(usuarioDB.Id, TypeController);

            // Realiza o commit no banco de dados
            _bancoContext.SaveChanges();

            return usuarioDB;
        }

    }
}
