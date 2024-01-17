﻿    using ControleDeContatos.Filters;
    using ControleDeContatos.Helper;
    using ControleDeContatos.Models.Requisicao;
    using ControleDeContatos.Models.Usuario;
    using ControleDeContatos.Repositorio;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

    namespace ControleDeContatos.Controllers
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
            public IActionResult BuscarComFiltros()
            {
                return View();
            }
            
            // ------------------------------------------- Metodos do modal de Creiar Requisicao -----------------------------------------------------------
            public IActionResult BuscaResponsavelPorNome(string txtSugestao)
            {
                
                List<UsuarioModel> result = _requisicaoRepositorio.BuscaResponsavelPorNome(txtSugestao);

                var dataFormat = result.Select(r => new { id_responsavel = r.Id, nome = r.Nome});

                return Json(dataFormat);
            }
            // ---------------------------------------------------------------------------------------------------------------------------------------------


            // --------------------------------------------------------------------- METODOS POST ----------------------------------------------------------
            [HttpPost]
            public IActionResult CriarReq(RequisicaoModel requisicao)
            {
                try 
                {
                    if (ModelState.IsValid)
                    {
                        _requisicaoRepositorio.CriarRequisicao( requisicao);
                        TempData["MensagemSucesso"] = $"Requisição criada com sucesso!";
                        return RedirectToAction("Index");
                    }
                    TempData["MensgemErro"] = $"Erro ao cadastrar uma nova requisição, verifique os campos e tente novamente!";
                    return RedirectToAction("Index");
                }
                catch (Exception error)
                {
                    TempData["MensgemErro"] = $"Erro ao consultar suas requisições, detalhes do erro({error.Message})";
                    return RedirectToAction("Index");
                }
            }
        }
    }
