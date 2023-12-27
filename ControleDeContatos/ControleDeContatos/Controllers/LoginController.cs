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
        private readonly IEnviaEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEnviaEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
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

        public IActionResult RecuperarSenha()
        {
            return View();
        }

        public IActionResult Sair()
        { 
            // Remove a sessão do usuario e redireciona para a tela de login
            _sessao.RemoveSessaoDoUsuario();
            return RedirectToAction("Index", "Login"); 
        }

        // ---------------------------------------------------------- Metodos Post -----------------------------------------------------
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
                TempData["MensgemErro"] = $"Erro ao tentar realizar o login, tente novamente, erro:{error.Message}";
                return RedirectToAction("Index");
            } 
        }

        [HttpPost]
        //Metodo que realiza o envio de email para a recuperação de senha, no caso ele gera uma senha nova para o usuario acessar e poder alterar sua senha novamente
        public IActionResult RecuperarSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                //Verufica se os valores enviados para model são validos
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailLogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string novasenha = usuario.GerarNovaSenha();

                        string msg = $"Prezado(a) {usuario.Nome}, sua nova senha para acesso ao sistema Connecting All é : {novasenha}, após o acesso é recomendado que o mesmo realize a alteração para uma senha particular";

                        bool emailEnviado = _email.Enviar(usuario.Email, "Connecting All - Nova senha de acesso", msg);

                        if (emailEnviado)   
                        {   
                            usuario.Password = novasenha;
                            _usuarioRepositorio.AtualizarSenha(usuario);

                            TempData["MensagemSucesso"] = $"Enviamos uma nova senha para o e-mail {redefinirSenhaModel.Email}, ao acessar você pode alterar sua senha para uma de seu gosto";
                            return RedirectToAction("Index", "Login");
                        }
                        else
                        {
                            TempData["MensgemErro"] = "Não conseguimos enviar o e-mail para recuperar sua senha, Por favor tente novamente.";
                        }
                    }
                    TempData["MensgemErro"] = "Não conseguimos recuperar sua senha. Por favor, verifique os dados e tente novamente!";

                }

                return View("Index");

            }   
            catch (Exception error)
            {
                TempData["MensgemErro"] = $"Erro ao tentar recuperar a senha , tente novamente, erro:{error.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
