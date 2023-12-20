using ControleDeContatos.Filters;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ControleDeContatos.Controllers
{
    [PaginaParaUsuarioLogado]
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
            List<ContatoViewModel> contatos = _contatoRepositorio.BuscarTodos();

            return View(contatos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id) {

            //Chama o metodo que busca os dados por id e retorna para view
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);

            return View(contato);
        }

        public IActionResult PerfilCliente(int id)
        {
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);

            return View(contato);
        }

        public IActionResult Apagar(int id)
        {

            //Chama o metodo que busca os dados por id e retorna para view
            ContatoModel contato = _contatoRepositorio.ListarPorId(id);

            return View(contato);
        }

        // Metodos POST de inserção e manipulação 
        [HttpPost] // Realizando a assinatura do tipo que esse metodo pertence
        public IActionResult Criar(ContatoModel contato, IFormFile? picture_upload)
        {
            // Tratativa de erro ao tentar cadastrar contato
            try
            {
                // Verifica se os dados que vem da model seguem os requisitos pré-estabelecidos
                if (ModelState.IsValid)
                {
                    // Utilizando a variavel que contem o objeto de repositorio do banco
                    _contatoRepositorio.Adicionar(contato, picture_upload);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(contato);
            }
            // Caso aponte erro exibe a mensagem
            catch (Exception error)
            {
                TempData["MensgemErro"] = $"Erro ao cadastrar o contato, detalhe do erro: {error.Message}" ;
                return RedirectToAction("Index");
            }
        }
 
        [HttpPost] // Realizando a assinatura do tipo que esse metodo pertence
        public IActionResult Alterar(ContatoModel contato, IFormFile? picture_upload)
        {
            // Tratativa de erro ao tentar alterar contato
            try {

                if (ModelState.IsValid)
                {
                    // Utilizando a variavel que contem o objeto de repositorio do banco
                    _contatoRepositorio.Alterar(contato, picture_upload);
                    TempData["MensagemSucesso"] = "Contato editado com sucesso!";
                    return RedirectToAction("Index");
                }
                // Por conta do metodo não possuir o mesmo nome da view forçamos ele a redirecionar para a que desejamos
                return View("Editar", contato);
            } 
            catch (Exception error) {
                TempData["MensgemErro"] = $"Erro ao editar  contato, detalhe do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]  
        public IActionResult Excluir(ContatoModel contato) {

            // Tratativa de erro ao tentar apagar o contato
            try { 
            
                _contatoRepositorio.Excluir(contato);
                TempData["MensagemSucesso"] = "Contato apagado com sucesso!";
                return RedirectToAction("Index");
            } 
            catch (Exception error)
            {
                TempData["MensgemErro"] = $"Erro ao tentar apagr o contato, detalhe do erro: {error.Message}";
                return RedirectToAction("Index");
            }

        }
    }
}
