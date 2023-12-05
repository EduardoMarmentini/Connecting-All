using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        // Criando a varivel que ira receber as propriedades do contexto de banco
        private readonly BancoContext _bancoContext;
        private readonly IPhotoRepositorio _photo;

        // Criando o construtor e injetando o contexto no mesmo
        public ContatoRepositorio(BancoContext bancoContext, IPhotoRepositorio photo)
        {
            // Instanciando as dependecias do banco no caso a String de conexão
            _bancoContext = bancoContext;
            _photo = photo;
        }

        // Busca todos os dados referentes ao id passado.
        public ContatoModel ListarPorId(int id)
        {
            return _bancoContext.Contatos.FirstOrDefault(x => x.Id == id);
        }

        // Metodo que lista todos os registros 
        public List<ContatoModel> BuscarTodos()
        {
            // Busca o metodo ToList do EntityFrameworkCore que lista todas a informações da tabela desejada
            return _bancoContext.Contatos.ToList();
        }
        //Lista as informações pelo id do contato.

        // Gravar no banco de dados (Pelo contexto)
        ContatoModel IContatoRepositorio.Adicionar(ContatoModel contato, IFormFile picture_upload)
        {
            // Chama o metodo de gravar e seleciona a tabela desejada como .Contatos
            _bancoContext.Contatos.Add(contato);

            // Realiza o commmit no banco de dados
            _bancoContext.SaveChanges();

            // Salva a foto de perfil do usuario
            _photo.UploadPhoto(contato.Id, picture_upload);

            return contato;
        }

        public ContatoModel Alterar(ContatoModel contato, IFormFile picture_upload)
        {
            ContatoModel contatoDB = ListarPorId(contato.Id);

            if (contatoDB == null) throw new Exception("Houve um erro na alteração do contato");

            contatoDB.Nome    = contato.Nome;
            contatoDB.Email   = contato.Email;
            contatoDB.Celular = contato.Celular;

            // Chama o metodo de atualizar os dados do banco do entityFrameworkCore 
            _bancoContext.Contatos.Update(contatoDB);

            // Chama o metodo de atualizar a foto de perfil
            _photo.AlterarPhoto(contato.Id, picture_upload);
            // Realiza o commit no banco de dados
            _bancoContext.SaveChanges();

            return contatoDB;

        }

        public ContatoModel Excluir(ContatoModel contato)
        {
            ContatoModel contatoDB = ListarPorId(contato.Id);

            if (contatoDB == null) throw new Exception("Houve um erro na exclusão do contato");

            // Chama o metodo que exclui a foto de perfil
            _photo.ExcluirPhoto(contato.Id);
            throw new NotImplementedException();
        }
    }
}
