using ProjetoProdutos.Enums;
using System.ComponentModel.DataAnnotations;

namespace ProjetoProdutos.Models
{
    public class mdlUsuarioSemSenha
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public PerfilEnum? TipoPerfil { get; set; }
    }
}
