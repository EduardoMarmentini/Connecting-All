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

                    var requisicoesFormatadas = requisicoes.Select(r => new
                    {
                        id_requisicao = Convert.ToInt32(r.requisicao.id_requisicao),
                        id_usuario = Convert.ToInt32(r.requisicao.id_usuario),
                        responsavel = Convert.ToString(r.requisicao.responsavel),
                        titulo_requisicao = Convert.ToString(r.requisicao.titulo_requisicao),
                        status = Convert.ToString(r.requisicao.status),
                        cliente = Convert.ToString(r.cliente.Nome),
                        data_cadastro = r.requisicao.data_cadastro.ToString("dd/MM/yyyy"), 
                        data_entrega = r.requisicao.data_entrega.ToString("dd/MM/yyyy"), 
                        prioridade = Convert.ToString(r.prioridade.descricao),
                        color_prioridade = Convert.ToString(r.prioridade.color),
                        horas_trabalhadas = r.requisicao.horas_trabalhadas.ToString("HH:mm:ss"),
                        descricao = Convert.ToString(r.statusReq.descricao),
                        color_status = Convert.ToString(r.statusReq.color)
                    });

                    // Retorne os dados formatados diretamente como JSON
                    return Json(requisicoesFormatadas);
            }
                catch (Exception error)
                {
                    TempData["MensgemErro"] = $"Erro ao consultar suas requisições, detalhes do erro({error.Message})";
                    return RedirectToAction("Index");
                }
            }
        }
    }
