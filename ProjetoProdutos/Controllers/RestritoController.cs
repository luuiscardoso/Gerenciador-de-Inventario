using Microsoft.AspNetCore.Mvc;
using ProjetoProdutos.Filters;

namespace ProjetoProdutos.Controllers
{
    [PaginaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
