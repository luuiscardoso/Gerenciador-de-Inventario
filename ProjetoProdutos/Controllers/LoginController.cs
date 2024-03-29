using Microsoft.AspNetCore.Mvc;
using ProjetoProdutos.Helper;
using ProjetoProdutos.Models;
using ProjetoProdutos.Repositorio;

namespace ProjetoProdutos.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;
        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;
            _email = email;
        }
        public IActionResult Index()
        {
            // se usuario estiver logado, redirecionar para a home

            if(_sessao.VerSessaoUsuario()  != null) return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public IActionResult Entrar(mdlLogin dadosLogin)
        {
            try
            {
                if(ModelState.IsValid)
                {

                    mdlUsuario usuarioDB = _usuarioRepositorio.VerPorLogin(dadosLogin.Login);

                    if (usuarioDB != null) // usuario existe
                    {

                        bool resultado = _usuarioRepositorio.ValidarSenha(usuarioDB, dadosLogin.Senha);
                        if (resultado) {
                            _sessao.CriarSessaoUsuario(usuarioDB);
                            return RedirectToAction("Index", "Home"); 
                        } 
                        else 
                        { 
                            TempData["Erro"] = "Senha invalida.";
                        }

                    }
                    else
                    {
                        TempData["Erro"] = "Usuario nao encontrado.";
                    }

                }

                return View("Index");
            }
            catch(Exception ex)
            {
                TempData["Erro"] = "Houve um erro ao tentar efetuar o login";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();

            return RedirectToAction("Index", "Login");
        }

        public IActionResult RedefinirSenha()
        {
            return View();
        }


        [HttpPost]
        public IActionResult EnviarLinkRedefinicao(mdlRedefinirSenha dados)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    mdlUsuario usuarioDB = _usuarioRepositorio.VerPorEmailELogin(dados.Email, dados.Login);

                    if (usuarioDB != null) // usuario existe
                    {
                        string senhaNova = usuarioDB.NovaSenha();
                        string mensagem = $"Sua nova senha é: {senhaNova}. Faça o login e altere em 'Alterar Senha'";

                        bool enviado = _email.Enviar(usuarioDB.Email, "Redefinição de senha - Sistema de estoque.", mensagem);

                        if (enviado)
                        {
                            _usuarioRepositorio.Atualizar(usuarioDB);
                            TempData["Sucesso"] = "Enviamos para o e-mail cadastrado uma nova senha.";
                        } else
                        {
                            TempData["Erro"] = "Nao foi possivel enviar o e-mail. Tente novamente.";
                        }

                        return RedirectToAction("Index", "Login");
                    }
                    else
                    {
                        TempData["Erro"] = "Nao foi possivel redefinir a senha. Verifique os dados informados.";
                    }

                }

                return View("RedefinirSenha");
            }
            catch (Exception ex)
            {
                TempData["Erro"] = "Houve um erro ao tentar redefinir sua senha";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastrar(mdlCadastroUsuario dadosNovoUsuario)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    if (_usuarioRepositorio.VerPorLogin(dadosNovoUsuario.Login) != null) {
                        TempData["Erro"] = "Já existe uma conta registrada com esse login.";
                        return RedirectToAction("Cadastrar");
                    }

                    if (_usuarioRepositorio.VerPorEmail(dadosNovoUsuario.Email) != null) {
                        TempData["Erro"] = "Já existe uma conta registrada a esse e-mail.";
                        return RedirectToAction("Cadastrar");
                    } 

                    mdlUsuario usuario = new mdlUsuario
                    {
                        Nome = dadosNovoUsuario.Nome,
                        Login = dadosNovoUsuario.Login,
                        Email = dadosNovoUsuario.Email,
                        Senha = dadosNovoUsuario.Senha,
                        TipoPerfil = Enums.PerfilEnum.Admin
                    };

                    _usuarioRepositorio.Adicionar(usuario);
                    TempData["Sucesso"] = "Seu cadastro foi realizado, faça o login agora mesmo.";
                    return RedirectToAction("Index");
                }
                return View(dadosNovoUsuario);
            }
            catch (Exception)
            {
                TempData["Erro"] = "Houve um erro inesperado e não conseguimos realizar seu cadastro. Tente novamente mais tarde";
                return RedirectToAction("Index");
            }
        }
    }
}
