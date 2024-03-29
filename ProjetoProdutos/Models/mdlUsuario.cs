using ProjetoProdutos.Enums;
using ProjetoProdutos.Helper;
using System.ComponentModel.DataAnnotations;

namespace ProjetoProdutos.Models
{
    public class mdlUsuario
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public PerfilEnum? TipoPerfil { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        public List<mdlProduto>? Produtos { get; set; }

        public void AddSenhaHash()
        {
            Senha = Senha.GerarHash();
        }

        public string NovaSenha()
        {
            string senhaNova = Guid.NewGuid().ToString().Substring(0, 8);
            Senha = senhaNova.GerarHash();
            return senhaNova;
        }
    }
}
