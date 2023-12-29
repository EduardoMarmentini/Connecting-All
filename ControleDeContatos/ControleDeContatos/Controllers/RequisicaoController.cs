using ControleDeContatos.Filters;
using ControleDeContatos.Helper;
using ControleDeContatos.Models.Requisicao;
using ControleDeContatos.Models.Usuario;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    [PaginaParaUsuarioLogado]
    public class RequisicaoController : Controller
    {
        private readonly IRequisicaoRepositorio _requisicao;
        private readonly ISessao _sessao;

        public RequisicaoController(IRequisicaoRepositorio requisicao, ISessao sessao) 
        { 
        
            _requisicao = requisicao;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            UsuarioModel usuario = _sessao.GetSessaoDoUsuario();

            List<RequisicaoViewModel> requisicoes = _requisicao.BuscarRequisicaoPorUsuario(usuario.Id);
            return View(requisicoes);
        }
    }
}
