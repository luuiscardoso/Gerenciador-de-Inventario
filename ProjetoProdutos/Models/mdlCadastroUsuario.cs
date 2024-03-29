using System.ComponentModel.DataAnnotations;

namespace ProjetoProdutos.Models
{
    public class mdlCadastroUsuario
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome {  get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Compare("Senha", ErrorMessage = "As senhan não correspondem.")]
        public string ConfirmaSenha { get; set; }

    }
}
