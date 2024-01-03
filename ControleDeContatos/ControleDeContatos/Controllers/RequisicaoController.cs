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
                return View();
            }

            public IActionResult BuscarRequisicoesUsuarioLogado()
            {
                try
                {
                    UsuarioModel usuario = _sessao.GetSessaoDoUsuario();
                    List<RequisicaoViewModel> requisicoes = _requisicao.BuscarRequisicaoPorUsuario(usuario.Id);

                    // Retorne os dados diretamente como JSON
                    return Json(requisicoes);
                }
                catch (Exception error)
                {
                    TempData["MensgemErro"] = $"Erro ao consultar suas requisições, detalhes do erro({error.Message})";
                    return RedirectToAction("Index");
                }
            }
        }
    }
