using Microsoft.AspNetCore.Mvc;
using ProjetoProdutos.Helper;
using ProjetoProdutos.Models;
using ProjetoProdutos.Repositorio;

namespace ProjetoProdutos.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            List<mdlUsuario> todosUsuarios = _usuarioRepositorio.VerTodos();
            return View(todosUsuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(mdlUsuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (_usuarioRepositorio.VerPorLogin(usuario.Login) != null)
                    {
                        TempData["Erro"] = "Já existe uma conta registrada com esse login.";
                        return RedirectToAction("Criar");
                    }

                    if (_usuarioRepositorio.VerPorEmail(usuario.Email) != null)
                    {
                        TempData["Erro"] = "Já existe uma conta registrada a esse e-mail.";
                        return RedirectToAction("Criar");
                    }

                    _usuarioRepositorio.Adicionar(usuario);
                    TempData["Sucesso"] = "Usuario cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Houve um erro ao tentar cadastrar";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ExcluirConfirmacao(int id)
        {

            mdlUsuario usuario = _sessao.VerSessaoUsuario();
            List<mdlUsuario> todosUsuarios = _usuarioRepositorio.VerTodos().ToList();
            mdlUsuario usuarioDB = _usuarioRepositorio.VerPorId(id);

            if (usuarioDB != null)
            {
                TempData["UsuarioID"] = $"{usuarioDB.id}";
                TempData["Exclusao"] = $"Deseja realmente exluir o usuario {usuarioDB.Nome}?";

                return View("Index", todosUsuarios);
            }

            return View("Index", todosUsuarios);
        }

        public IActionResult Excluir(int id)
        {
            try
            {
                mdlUsuario usuarioDB = _usuarioRepositorio.VerPorId(id);
                mdlUsuario usuarioNaSessao = _sessao.VerSessaoUsuario();

                if (usuarioDB == null || usuarioDB.id == usuarioNaSessao.id) return Json(new { msg = "Não é possivel realizar essa operação." });

                _usuarioRepositorio.Excluir(usuarioDB);
                return Json(new { msg = "Usuario excluído com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { msg = "Houve um erro inesperado, tente novamente mais tarde." });
            }

        }

        public IActionResult Editar(int id)
        {
            mdlUsuario usuarioDB = _usuarioRepositorio.VerPorId(id);
            return View(usuarioDB);
        }

        [HttpPost]
        public IActionResult Alterar(mdlUsuarioSemSenha usuarioSemSenha)
        {
            try
            {

                mdlUsuario usuarioDB = null;

                if (ModelState.IsValid)
                {
                    usuarioDB = new mdlUsuario()
                    {
                        id = usuarioSemSenha.Id,
                        Nome = usuarioSemSenha.Nome,
                        Login = usuarioSemSenha.Login,
                        Email = usuarioSemSenha.Email,
                        TipoPerfil = usuarioSemSenha.TipoPerfil
                    };

                    _usuarioRepositorio.Atualizar(usuarioDB);
                    TempData["Sucesso"] = "Usuario alterado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View("Editar", usuarioDB);
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Houve um erro ao tentar editar";
                return RedirectToAction("Index");
            }
        }
    }
}
