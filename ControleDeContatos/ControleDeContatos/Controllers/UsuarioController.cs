using ControleDeContatos.Filters;
using ControleDeContatos.Helper;
using ControleDeContatos.Models.Requisicao;
using ControleDeContatos.Models.Usuario;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ControleDeContatos.Controllers
{
    public class UsuarioController : Controller
    {

        // Criando a varivel que ira receber as propriedades do repositorio de usuarios
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IRequisicaoRepositorio _requisicaoRepositorio;
        private readonly ISessao _sessao;


        // Criando o construtor para injetar as dependencias do repositorio que contem os metodos de manipulãção do banco de dados
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IRequisicaoRepositorio requisicaoRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _requisicaoRepositorio = requisicaoRepositorio;
            _sessao = sessao;
        }

        // Metodos que não possuem uma especificação de qual se tipo por padrão são metodos GET sendo assim apenas para busca de informações
        [PaginaSomenteAdmin]
        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }
        [PaginaSomenteAdmin]
        public IActionResult Criar() 
        {
            return View();
        }
        [PaginaSomenteAdmin]
        public IActionResult EditarUsuario(int id)
        {
            //Chama o metodo que busca os dados por id e retorna para view
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);

            return View(usuario);
        }
        [AllowAnonymous]
        public IActionResult EditarConta(int id)
        {
            //Chama o metodo que busca os dados por id e retorna para view
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);
            EditarContaModel viewModel = new EditarContaModel
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Login = usuario.Login,
                
            };
            return View(viewModel);
        }
        [AllowAnonymous]
        public IActionResult ContaUsuario(int id)
        {

            //Chama o metodo que busca os dados por id e retorna para view
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);

            return View(usuario);
        }
        [PaginaSomenteAdmin]
        public IActionResult PerfilUsuario(int id)
        {

            //Chama o metodo que busca os dados por id e retorna para view
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);

            return View(usuario);
        }
        [PaginaSomenteAdmin]
        public IActionResult Apagar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);

            return View(usuario);
        }

        public IActionResult BuscarCompromissosUsuarioLogado()
        {
            UsuarioModel usuario = _sessao.GetSessaoDoUsuario();
            List<RequisicaoViewModel> compromissos = _requisicaoRepositorio.BuscarRequisicaoPorUsuario(usuario.Id);

            var dataFormat = compromissos.Select(c => new 
                { 
                    title = $"Nº:{c.requisicao.id_requisicao} {c.requisicao.titulo_requisicao}",
                    data_entrega = c.requisicao.data_entrega.ToString("yyyy-MM-dd"),
                }
            );

            return Json(dataFormat);
        }

        // Metodos POST de inserção e manipulação 
        [HttpPost] // Realizando a assinatura do tipo que esse metodo pertence
        [PaginaSomenteAdmin]
        public IActionResult Criar(UsuarioModel usuario, IFormFile? picture_upload) 
        {
            try 
            {
                // Verifica se os dados que vem da model seguem os requisitos pré-estabelecidos
                if (ModelState.IsValid)
                {
                    // Utilizando a variavel que contem o objeto de repositorio do banco
                    _usuarioRepositorio.Adicionar(usuario, picture_upload);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            // Caso aponte erro exibe a mensagem
            catch (Exception error)
            {
                TempData["MensgemErro"] = $"Erro ao cadastrar o usuário, detalhe do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost] // Realizando a assinatura do tipo que esse metodo pertence
        [PaginaSomenteAdmin]
        public IActionResult AtualizarUsuario(UsuarioModel usuario, IFormFile? picture_upload)
        {
            // Tratativa de erro ao tentar alterar usuario
            try
            {

                if (ModelState.IsValid)
                {
                    // Se o campo de senha estiver vazio, defina a senha para o valor atual
                    if (string.IsNullOrEmpty(usuario.Password))
                    {
                        usuario.Password = null;
                    }

                    // Utilizando a variavel que contem o objeto de repositorio do banco
                    _usuarioRepositorio.AtualizarUsuario(usuario, picture_upload);
                    TempData["MensagemSucesso"] = "Usuário editado com sucesso!";
                }
                // Por conta do metodo não possuir o mesmo nome da view forçamos ele a redirecionar para a que desejamos
                return RedirectToAction("Index");
            }
            catch (Exception error)
            {
                TempData["MensgemErro"] = $"Erro ao editar  usuário, detalhe do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost] // Realizando a assinatura do tipo que esse metodo pertence
        [AllowAnonymous]
        public IActionResult AtualizarConta(EditarContaModel usuario, IFormFile? picture_upload)
        {
            // Tratativa de erro ao tentar alterar usuario
            try
            {
                if (ModelState.IsValid)
                {
                    // Se o campo de senha estiver vazio, defina a senha para o valor atual
                    if (string.IsNullOrEmpty(usuario.New_Password))
                    {
                        usuario.New_Password = null;
                    }

                    if(!usuario.ValidaNovaSenha())
                    {
                        TempData["MensgemErro"] = $"As senhas não são identicas, corriga para prosseguir";
                        return RedirectToAction("EditarConta", "Usuario");
                    }

                    // Utilizando a variavel que contem o objeto de repositorio do banco
                    _usuarioRepositorio.AtualizarConta(usuario, picture_upload);
                    TempData["MensagemSucesso"] = "Usuário editado com sucesso!";
                    return RedirectToAction("Index", "Home");
                }
                // Por conta do metodo não possuir o mesmo nome da view forçamos ele a redirecionar para a que desejamos
                return View("EditarUsuario", usuario);
            }
            catch (Exception error)
            {
                TempData["MensgemErro"] = $"Erro ao editar  usuário, detalhe do erro: {error.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        [PaginaSomenteAdmin]
        public IActionResult Excluir(UsuarioModel usuario)
        {

            // Tratativa de erro ao tentar apagar o contato
            try
            {

                _usuarioRepositorio.Excluir(usuario);
                TempData["MensagemSucesso"] = "Usuário apagado com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception error)
            {
                TempData["MensgemErro"] = $"Erro ao tentar apagar o usuário, detalhe do erro: {error.Message}";
                return RedirectToAction("Index");
            }


        }

    }
}
