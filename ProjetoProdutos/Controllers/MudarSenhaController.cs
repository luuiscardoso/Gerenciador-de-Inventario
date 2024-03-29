using Microsoft.AspNetCore.Mvc;
using ProjetoProdutos.Helper;
using ProjetoProdutos.Models;
using ProjetoProdutos.Repositorio;

namespace ProjetoProdutos.Controllers
{
    public class MudarSenhaController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        
        public MudarSenhaController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost] public IActionResult Alterar(mdlAlteracaoSenha dados)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    mdlUsuario usuarioNaSessao = _sessao.VerSessaoUsuario();

                    dados.Id = usuarioNaSessao.id;

                    _usuarioRepositorio.AtualizarSenha(dados);
                    TempData["Sucesso"] = "Senha alterada com sucesso.";
                    return RedirectToAction("Index", "Home");
                }

                return View("Index", dados);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = $"Não foi possviel alterar a senha, tente novamente. Detalhes: {ex.Message}";
                return View("Index", dados);
            }
        }
    }
}
