using Microsoft.EntityFrameworkCore;
using ProjetoProdutos.Data;
using ProjetoProdutos.Helper;
using ProjetoProdutos.Models;

namespace ProjetoProdutos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BdContext _bdcontext;
        public UsuarioRepositorio(BdContext bdcontext) { 
            _bdcontext = bdcontext;
        }

        public mdlUsuario VerPorEmailELogin(string email, string login) => _bdcontext.Usuarios.FirstOrDefault(r => r.Login == login && r.Email == email);

        public mdlUsuario VerPorLogin(string login) => _bdcontext.Usuarios.FirstOrDefault(r => r.Login == login);

        public mdlUsuario VerPorEmail(string email) => _bdcontext.Usuarios.FirstOrDefault(r => r.Email == email);
        public mdlUsuario VerPorId(int id) => _bdcontext.Usuarios.FirstOrDefault(r => r.id == id);
        public List<mdlUsuario> VerTodos() => _bdcontext.Usuarios.ToList();
        public mdlUsuario Adicionar(mdlUsuario usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            usuario.AddSenhaHash();
            _bdcontext.Usuarios.Add(usuario);
            _bdcontext.SaveChanges();
            return usuario;
            // gravar no banco de dados
        }

        public void Excluir(mdlUsuario usuario)
        {
            mdlUsuario usuarioProdutos = _bdcontext.Usuarios.Include(e => e.Produtos).First();
            _bdcontext.Usuarios.Remove(usuarioProdutos);
            _bdcontext.SaveChanges();
        }


        public mdlUsuario Atualizar(mdlUsuario usuario)
        {

            mdlUsuario usuarioDB = VerPorId(usuario.id);

            if (usuarioDB == null) throw new Exception("Erro ao alterar o usuário.");

            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Email = usuario.Email;
            usuarioDB.Login = usuario.Login;
            usuarioDB.TipoPerfil = usuario.TipoPerfil;
            usuarioDB.DataAtualizacao = DateTime.Now;

            _bdcontext.Usuarios.Update(usuarioDB);
            _bdcontext.SaveChanges();
            return usuarioDB;
        }

        public bool ValidarSenha(mdlUsuario usuarioDB, string senha) => usuarioDB.Senha == senha.GerarHash();

        public mdlUsuario AtualizarSenha(mdlAlteracaoSenha dados)
        {
            mdlUsuario usuarioDB = VerPorId(dados.Id);

            if (usuarioDB == null) throw new Exception("Erro ao encontrar esse usuário.");

            if(!ValidarSenha(usuarioDB, dados.SenhaAtual)) throw new Exception("Senha inválida.");

            if (ValidarSenha(usuarioDB, dados.NovaSenha)) throw new Exception("Informe uma senha diferente da atual.");

            usuarioDB.Senha = dados.NovaSenha.GerarHash();
            usuarioDB.DataAtualizacao = DateTime.Now;

            _bdcontext.Usuarios.Update(usuarioDB);
            _bdcontext.SaveChanges();

            return usuarioDB;
        }
    }
}
