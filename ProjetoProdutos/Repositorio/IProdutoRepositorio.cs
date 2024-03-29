using ProjetoProdutos.Models;

namespace ProjetoProdutos.Repositorio
{
    public interface IProdutoRepositorio
    {
        mdlProduto VerPorId(int id);
        List<mdlProduto> VerTodos(int usuarioId);
        mdlProduto Adicionar(mdlProduto produto);

        mdlProduto Atualizar (mdlProduto produto);
        void Excluir(mdlProduto prodDB);
    }
}
