using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        // Criando a varivel que ira receber as propriedades do contexto de banco

        private readonly BancoContext _bancoContext;

        // Criando o construtor e injetando o contexto no mesmo
        public ContatoRepositorio(BancoContext bancoContext)
        {
            // Instanciando as dependecias do banco no caso a String de conexão
            _bancoContext = bancoContext;
        }

        // Metodo que lista todos os registros 
        public List<ContatoModel> BuscarTodos()
        {
            // Busca o metodo ToList do EntityFrameworkCore que lista todas a informações da tabela desejada
            return _bancoContext.Contatos.ToList();
        }

        // Gravar no banco de dados (Pelo contexto)
        ContatoModel IContatoRepositorio.Adicionar(ContatoModel contato, IFormFile picture_upload)
        {
            // Chama o metodo de gravar e seleciona a tabela desejada como .Contatos
            _bancoContext.Contatos.Add(contato);
            // Realiza o commmit no banco de dados
            _bancoContext.SaveChanges();

            // Salva a imagem na pasta 'img' com o nome do arquivo sendo o id_do_contato.extensão_do_arquivo
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", contato.Id + Path.GetExtension(picture_upload.FileName));
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                picture_upload.CopyTo(stream);
            }
            return contato;
        }
    }
}
