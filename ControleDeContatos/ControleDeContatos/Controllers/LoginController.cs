using ControleDeContatos.Helper;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            // Caso o usuário ja estiver logado será redirecionado para home automaticamente 
            if (_sessao.GetSessaoDoUsuario() != null) 
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public IActionResult Sair()
        { 
            // Remove a sessão do usuario e redireciona para a tela de login
            _sessao.RemoveSessaoDoUsuario();
            return RedirectToAction("Index", "Login"); 
        }

        // Metodos Post
        [HttpPost]
        // Metodo para realizar a validação do login do usuario
        public IActionResult Entrar(LoginModel loginModel) 
        { 
            try 
            {
                //Verufica se os valores enviados para model são validos
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario =  _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if (usuario != null)
                   {
                        if(usuario.ValidaSenha(loginModel.Password))
                        {
                            // Metodo para criar a sessão de usuario e seus cookies
                            _sessao.CriarSessaoDoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensgemErro"] = "Senha invalida. Por favor, tente novamente!";
                    }
                    TempData["MensgemErro"] = "Login e/ou senha invalido(s). Por favor, tente novamente!";
                   
                }

                return View("Index");

            } 
            catch (Exception error) 
            {
                TempData["MensgemErro"] = $"Erro ao tentar realizar o logon, tente novamente, erro:{error.Message}";
                return RedirectToAction("Index");
            } 
        }
    }
}
