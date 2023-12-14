using ControleDeContatos.Filters;
using ControleDeContatos.Models;
using ControleDeContatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeContatos.Controllers
{
    [PaginaSomenteAdmin]
    public class UsuarioController : Controller
    {

        // Criando a varivel que ira receber as propriedades do repositorio de usuarios
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        // Criando o construtor para injetar as dependencias do repositorio que contem os metodos de manipulãção do banco de dados
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        // Metodos que não possuem uma especificação de qual se tipo por padrão são metodos GET sendo assim apenas para busca de informações
        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodos();
            return View(usuarios);
        }

        public IActionResult Criar() 
        {
            return View();
        }

        public IActionResult EditarUsuario(int id)
        {

            //Chama o metodo que busca os dados por id e retorna para view
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);

            return View(usuario);
        }

        public IActionResult EditarConta(int id)
        {

            //Chama o metodo que busca os dados por id e retorna para view
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);

            return View(usuario);
        }

        public IActionResult Apagar(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.ListarPorId(id);

            return View(usuario);
        }

        // Metodos POST de inserção e manipulação 
        [HttpPost] // Realizando a assinatura do tipo que esse metodo pertence
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
                    _usuarioRepositorio.AtualizarConta(usuario, picture_upload);
                    TempData["MensagemSucesso"] = "Usuário editado com sucesso!";
                    return RedirectToAction("Index");
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

        [HttpPost] // Realizando a assinatura do tipo que esse metodo pertence
        public IActionResult AtualizarConta(UsuarioModel usuario, IFormFile? picture_upload)
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
                    _usuarioRepositorio.AtualizarConta(usuario, picture_upload);
                    TempData["MensagemSucesso"] = "Usuário editado com sucesso!";
                    return RedirectToAction("Index");
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
