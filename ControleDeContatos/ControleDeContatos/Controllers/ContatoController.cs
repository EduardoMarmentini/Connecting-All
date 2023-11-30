using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class ContatoController : Controller
    {
        // Criando a varivel que ira receber as propriedades do repositorio de contatos
        private readonly IContatoRepositorio _contatoRepositorio;

        // Criando o construtor para injetar as dependencias do repositorio que contem os metodos de manipulãção do banco de dados
        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;
        }

        // Metodos que não possuem uma especificação de qual se tipo por padrão são metodos GET sendo assim apenas para busca de informações
        public IActionResult Index()
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodos();

            return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Apagar() {
            return View(); 
        }

        public IActionResult Editar(int id) {

            //Chama o metodo que busca os dados por id e retorna para view
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);

            return View(contato);
        }

        // Metodos POST de inserção e manipulação 
        [HttpPost] // Realizando a assinatura do tipo que esse metodo pertence
        public IActionResult Criar(ContatoModel contato, IFormFile picture_upload)
        {
            // Utilizando a variavel que contem o objeto de repositorio do banco
            _contatoRepositorio.Adicionar(contato, picture_upload);
            return RedirectToAction("Index");
        }

        // Metodos POST de inserção e manipulação 
        [HttpPost] // Realizando a assinatura do tipo que esse metodo pertence
        public IActionResult Alterar(ContatoModel contato, IFormFile picture_upload)
        {
            // Utilizando a variavel que contem o objeto de repositorio do banco
            _contatoRepositorio.Alterar(contato, picture_upload);
            return RedirectToAction("Index");
        }
    }
}
