using Microsoft.AspNetCore.Mvc;
using ProjetoProdutos.Data;
using ProjetoProdutos.Helper;
using ProjetoProdutos.Models;
using ProjetoProdutos.Repositorio;
using System.Collections.Generic;

namespace ProjetoProdutos.Controllers
{
    public class ProdutoController : Controller
    {
        private readonly IProdutoRepositorio _produtoRepositorio;
        private readonly ISessao _sessao;
        public ProdutoController(IProdutoRepositorio produtoRepositorio, ISessao sessao) {
            _produtoRepositorio = produtoRepositorio;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            mdlUsuario usuario = _sessao.VerSessaoUsuario();
            List<mdlProduto> todosProdutos = _produtoRepositorio.VerTodos(usuario.id);
            return View(todosProdutos);
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            mdlProduto prod = _produtoRepositorio.VerPorId(id);
            return View(prod);
        }

        public IActionResult ExcluirConfirmacao(int id)
        {
            mdlUsuario usuario = _sessao.VerSessaoUsuario();
            List <mdlProduto> allProds = _produtoRepositorio.VerTodos(usuario.id).ToList();
            mdlProduto prodDB = _produtoRepositorio.VerPorId(id);

            if (prodDB != null) { 
                TempData["ProdutoID"] = $"{prodDB.Id}";
                TempData["Exclusao"] = $"Deseja realmente exluir o produto {prodDB.Nome}?";
            
                return View("Index", allProds);
            }

            return View("Index", allProds); 
        }

        [HttpPost]
        public IActionResult Criar(mdlProduto produto) {
            try
                {
                if (ModelState.IsValid)
                {
                    mdlUsuario usuario = _sessao.VerSessaoUsuario();
                    produto.UsuarioID = usuario.id;

                    _produtoRepositorio.Adicionar(produto);
                    TempData["Sucesso"] = "Produto cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(produto);
            } 
            catch (Exception ex)
            {
                TempData["Erro"] = "Houve um erro ao tentar cadastrar";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Alterar(mdlProduto produto)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    mdlUsuario usuario = _sessao.VerSessaoUsuario();
                    produto.UsuarioID = usuario.id;

                    _produtoRepositorio.Atualizar(produto);
                    TempData["Sucesso"] = "Produto alterado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View("Editar", produto);
            } 
            catch (Exception ex)
            {
                TempData["Erro"] = "Houve um erro ao tentar editar";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Excluir(int id)
        {
            try
            {
                mdlProduto prodDB = _produtoRepositorio.VerPorId(id);

                if (prodDB == null) return Json(new { msg = "Erro ao excluir." });

                _produtoRepositorio.Excluir(prodDB);
                return Json(new { msg = "Produto excluído com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { msg = "Houve um erro inesperado, tente novamente mais tarde." });
            }
            
        }

    }
}
