using Microsoft.AspNetCore.Mvc;
using ProjetoProdutos.Filters;
using ProjetoProdutos.Models;
using System.Diagnostics;

namespace ProjetoProdutos.Controllers
{
    [PaginaUsuarioLogado] /* antes de bater aqui nessa controler e executar alguma ação, ele vai executar o codigo desse filtro
    para verificar se o usuario ta logado*/
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}