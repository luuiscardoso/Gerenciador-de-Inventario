using ProjetoProdutos.Models;

namespace ProjetoProdutos.Helper
{
    public interface ISessao
    {
        void CriarSessaoUsuario(mdlUsuario usuario);
        void RemoverSessaoUsuario();
        mdlUsuario VerSessaoUsuario();
    }
}
