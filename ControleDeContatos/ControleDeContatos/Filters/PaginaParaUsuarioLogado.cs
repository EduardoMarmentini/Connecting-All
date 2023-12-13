using ControleDeContatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace ControleDeContatos.Filters
{
    public class PaginaParaUsuarioLogado : ActionFilterAttribute
    {
        // Filtro de acesso para as rotas, somente se o usuario tiver uma sessão logada ele podera acessar as rotas dentro do sistema
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // Busca os cookies de sessão do usuario
            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");

            // Verifica se a sessão existe
            if(string.IsNullOrEmpty(sessaoUsuario))
            {
                // Caso não exista joga para a tela de login 
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { {"controller", "Login"}, {"action", "Index"} });
            }
            else 
            {
                // Caso a sessão exista ele converte os dados da sessão do usuario e verifica se o mesmo existe
                UsuarioModel usuario = JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);

                if (usuario == null) 
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }
            }

            base.OnActionExecuted(context); 
        }
    }
}
