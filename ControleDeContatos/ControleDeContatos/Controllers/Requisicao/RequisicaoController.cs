using ControleDeContatos.Filters;
using ControleDeContatos.Helper;
using ControleDeContatos.Models.Requisicao;
using ControleDeContatos.Models.Usuario;
using ControleDeContatos.Repositorio.Requisicao;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControleDeContatos.Controllers.Requisicao
{
    [PaginaParaUsuarioLogado]
    public class RequisicaoController : Controller
    {
        private readonly IRequisicaoRepositorio _requisicaoRepositorio;
        private readonly ISessao _sessao;

        public RequisicaoController(IRequisicaoRepositorio requisicao, ISessao sessao)
        {

            _requisicaoRepositorio = requisicao;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            return View();
        }

        // Metodo que busca as requisições pertencentes ao usuario logado
        public IActionResult BuscarRequisicoesUsuarioLogado()
        {
            try
            {
                UsuarioModel usuario = _sessao.GetSessaoDoUsuario();
                List<RequisicaoViewModel> requisicoes = _requisicaoRepositorio.BuscarRequisicaoPorUsuario(usuario.Id);

                var dataFormat = requisicoes.Select(r => new
                {
                    id_requisicao = Convert.ToInt32(r.requisicao.id_requisicao),
                    id_usuario = Convert.ToInt32(r.requisicao.id_usuario),
                    responsavel = Convert.ToString(r.usuario.Nome),
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
                return Json(dataFormat);
            }
            catch (Exception error)
            {
                TempData["MensgemErro"] = $"Erro ao consultar suas requisições, detalhes do erro({error.Message})";
                return RedirectToAction("Index");
            }
        }
        // Metodo que busca pelos filtros passados, como a situação, o usuario responsavel e o numero da demanda
        public IActionResult BuscarComFiltros(int id_requisicao, string titulo_requisicao, int id_usuario, int id_cliente, int id_estado_req)
        {
            try
            {
                List<RequisicaoViewModel> requisicoes = _requisicaoRepositorio.BuscarRequisicaoPorFiltros(id_requisicao, titulo_requisicao, id_usuario, id_cliente, id_estado_req);

                var dataFormat = requisicoes.Select(r => new
                {
                    id_requisicao = Convert.ToInt32(r.requisicao.id_requisicao),
                    id_usuario = Convert.ToInt32(r.requisicao.id_usuario),
                    responsavel = Convert.ToString(r.usuario.Nome),
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
                return Json(dataFormat);
            }

            catch (Exception error)
            {
                TempData["MensgemErro"] = $"Erro ao consultar suas requisições, detalhes do erro({error.Message})";
                return RedirectToAction("Index");
            }
        }

        // ------------------------------------------- Metodos do modal de Creiar Requisicao -----------------------------------------------------------
        public IActionResult BuscaResponsavelPorNome(string txtSugestao)
        {

            List<UsuarioModel> result = _requisicaoRepositorio.BuscaResponsavelPorNome(txtSugestao);

            var dataFormat = result.Select(r => new { id_responsavel = r.Id, nome = r.Nome });

            return Json(dataFormat);
        }

        public IActionResult BuscarOcorrenciasPorReq(int id_requisicao)
        {
            List<RequisicaoOcorrenciaModel> result = _requisicaoRepositorio.BuscarOcorrenciasPorReq(id_requisicao);

            var dataFormat = result;

            return Json(dataFormat);
        }
        // ---------------------------------------------------------------------------------------------------------------------------------------------


        // --------------------------------------------------------------------- METODOS POST ----------------------------------------------------------
        [HttpPost]
        public IActionResult CriarReq(CriarReqModel requisicao)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _requisicaoRepositorio.CriarRequisicao(requisicao);
                    TempData["MensagemSucesso"] = $"Requisição criada com sucesso!";
                    return RedirectToAction("Index");
                }
                TempData["MensgemErro"] = $"Erro ao cadastrar uma nova requisição, verifique os campos e tente novamente!";
                return RedirectToAction("Index");
            }
            catch (Exception error)
            {
                TempData["MensgemErro"] = $"Erro ao cadastrar suas requisições, detalhes do erro({error.Message})";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult EncaminharReq(RequisicaoOcorrenciaModel registro)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    _requisicaoRepositorio.EncaminharRequisicao(registro);
                    TempData["MensagemSucesso"] = $"Requisição encaminhada com sucesso!";
                    return RedirectToAction("Index");
                }
                TempData["MensgemErro"] = $"Erro ao encaminhar uma nova requisição, verifique os campos e tente novamente!";
                return RedirectToAction("Index");
            }
            catch (Exception error)
            {
                TempData["MensgemErro"] = $"Erro ao encaminhar a requisições, detalhes do erro({error.Message})";
                return RedirectToAction("Index");
            }
        }

    }
}
