using ControleDeContatos.Models.Usuario;
using Microsoft.AspNetCore.Http; // Adicione esta diretiva
using Newtonsoft.Json; // Adicione esta diretiva

namespace ControleDeContatos.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContext;

        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public void CriarSessaoDoUsuario(UsuarioModel usuario)
        {
            // Transforma o objeto usuario em uma string para ser lida e criada a sessão so usuário
            string value = JsonConvert.SerializeObject(usuario);
            _httpContext.HttpContext.Session.SetString("sessaoUsuarioLogado", value);
        }

        public UsuarioModel GetSessaoDoUsuario()
        {
            // Busca a sessão do usuario por meio da chave que guarda ela 
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("sessaoUsuarioLogado");

            // Verifica a sessão existe ou é vazia/nulla
            if(string.IsNullOrEmpty(sessaoUsuario))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);

        }

        public void RemoveSessaoDoUsuario()
        {
            // Remove a sessão do usuario logado
            _httpContext.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }
    }
}
