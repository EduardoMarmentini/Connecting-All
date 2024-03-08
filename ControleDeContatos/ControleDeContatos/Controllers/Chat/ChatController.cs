using ControleDeContatos.Filters;
using ControleDeContatos.Models.Chat;
using ControleDeContatos.Models.Cliente;
using ControleDeContatos.Repositorio.Clientes;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers.Chat
{
    [PaginaParaUsuarioLogado]
    public class ChatController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;

        public ChatController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio;

        }
        public IActionResult Index()
        {
            List<ContatoViewModel> contatos = _contatoRepositorio.BuscarTodos();

            return View(contatos);
        }

        public IActionResult Chat_Privado()
        {
            return View();
        }
    }
}
