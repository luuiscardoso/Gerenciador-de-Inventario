using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjetoProdutos.Models;
using System.Text.Json;

namespace ProjetoProdutos.ViewComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");

            mdlUsuario usuario = JsonSerializer.Deserialize<mdlUsuario>(sessaoUsuario);

            return View(usuario);
        }
    }
}
