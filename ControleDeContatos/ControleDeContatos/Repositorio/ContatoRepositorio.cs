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
            
            if (picture_upload != null)
            {
                // Salva a imagem na pasta 'img' com o nome do arquivo sendo o id_do_contato.extensão_do_arquivo
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", contato.Id + Path.GetExtension(picture_upload.FileName));
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    picture_upload.CopyTo(stream);
                }
            }

            // Realiza o commmit no banco de dados
            _bancoContext.SaveChanges();

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


            //Caminho onde será salva a foto
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", contato.Id + ".jpeg");

            // Verifica se a imagem já existe e, em caso afirmativo, exclui-a
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            // Caso o usuario tenha optado por trocar a foto ao inves de excluila ele atualiza a nova foto
            if (picture_upload != null)
            {
                // Salva a nova foto do contato
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    picture_upload.CopyTo(stream);
                }
            }

            // Realiza o commit no banco de dados
            _bancoContext.SaveChanges();

            return contatoDB;

        }
    }
}
