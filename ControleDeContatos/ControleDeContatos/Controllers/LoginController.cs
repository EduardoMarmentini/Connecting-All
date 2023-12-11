using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public LoginController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        public IActionResult Index()
        {
            return View();
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
