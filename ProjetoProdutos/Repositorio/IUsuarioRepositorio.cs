using ProjetoProdutos.Models;

namespace ProjetoProdutos.Repositorio
{
    public interface IUsuarioRepositorio
    {
        mdlUsuario VerPorEmailELogin(string email, string login);
        mdlUsuario VerPorLogin(string login);
        mdlUsuario VerPorId(int id);

        mdlUsuario VerPorEmail(string email);
        List<mdlUsuario> VerTodos();
        mdlUsuario Adicionar(mdlUsuario usuario);

        mdlUsuario Atualizar (mdlUsuario usuario);
        void Excluir(mdlUsuario usuario);
        bool ValidarSenha(mdlUsuario usuarioDB, string senha);

        mdlUsuario AtualizarSenha(mdlAlteracaoSenha dados);
    }
}
