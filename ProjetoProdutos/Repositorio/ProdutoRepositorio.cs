using ProjetoProdutos.Data;
using ProjetoProdutos.Models;

namespace ProjetoProdutos.Repositorio
{
    public class ProdutoRepositorio : IProdutoRepositorio
    {
        private readonly BdContext _bdcontext;
        public ProdutoRepositorio(BdContext bdcontext) { 
            _bdcontext = bdcontext;
        }
        public List<mdlProduto> VerTodos(int usuarioId)
        {
            return _bdcontext.Produtos.Where(r => r.UsuarioID == usuarioId).ToList();
        }
        public mdlProduto Adicionar(mdlProduto produto)
        {
            _bdcontext.Produtos.Add(produto);
            _bdcontext.SaveChanges();
            return produto;
        }

        public void Excluir(mdlProduto prod)
        {
            _bdcontext.Produtos.Remove(prod);
            _bdcontext.SaveChanges();
        }

        public mdlProduto VerPorId(int id)
        {
            return _bdcontext.Produtos.FirstOrDefault(x => x.Id == id);
        }

        public mdlProduto Atualizar(mdlProduto produto)
        {
            mdlProduto prodDB = VerPorId(produto.Id);

            if (prodDB == null) throw new Exception("Erro ao alterar o produto.");

            prodDB.Nome = produto.Nome;
            prodDB.Qtd = produto.Qtd;
            prodDB.Categoria = produto.Categoria;

            _bdcontext.Produtos.Update(prodDB);
            _bdcontext.SaveChanges();
            return prodDB;
        }
    }
}
