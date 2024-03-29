using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ProjetoProdutos.Models;
using System.Text.Json;

namespace ProjetoProdutos.Filters
{
    public class PaginaAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado"); // verifica se tem um usuario naquela sessao

            if (string.IsNullOrEmpty(sessaoUsuario)){ // n tem
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login"}, {"action", "Index" } });
            } else
            {
                mdlUsuario usuario = JsonSerializer.Deserialize<mdlUsuario>(sessaoUsuario);
                if (usuario == null) {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }

                if(usuario.TipoPerfil != Enums.PerfilEnum.Admin)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restrito" }, { "action", "Index" } });
                }
            }

            base.OnActionExecuted(context);
        }
    }
}
