using ProjetoProdutos.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjetoProdutos.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContext; // httpcontext fornece detalhes da solicitacao http atual
        public Sessao (IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }
        public void CriarSessaoUsuario(mdlUsuario usuario) 
        {
            string usuarioJson = JsonSerializer.Serialize(usuario); // convertendo o obj em JSON
            _httpContext.HttpContext.Session.SetString("sessaoUsuarioLogado", usuarioJson); /*guardando esse json usuario na sessao com a chave "sessaoUsuarioLogado"*/
        }

        public void RemoverSessaoUsuario()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }

        public mdlUsuario VerSessaoUsuario()
        {
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if(string.IsNullOrEmpty(sessaoUsuario)) return null;

            return JsonSerializer.Deserialize<mdlUsuario>(sessaoUsuario);
        }
    }
}

/*em app web, uma sessao eh o periodo de interação entre nos usuarios e o servidor, ela começa quando o luis acessa o site e termina
 quando ele sai ou sessao expira. Durante essa sessão (ler-se interação) sao armazenados dados temporarios no lado do servidor que sao dados
assosiados a sessao do luis. Basicamente a sessao representa o periodo de interação do usuario e servidor, armazena dados dele
enquanto ele navega. Cada sessao tem um id que é guardado em um cookie. 

Portanto, não se trata de criar manualmente uma sessão do zero, mas sim de iniciar uma sessão dinamicamente quando o usuário interage com a aplicação pela primeira vez e, 
a partir desse ponto, adicionar informações a essa sessão ou recuperar informações dela, conforme a interação do usuário progride durante sua sessão na aplicação

cookies sao pequenos arquivos de texto armazenados no navegador do usuario, esses arquivos permitem q sites armazenem informacoes no navegador 
para varias coisas. 
 */
