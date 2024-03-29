using System.ComponentModel.DataAnnotations;

namespace ProjetoProdutos.Models
{
    public class mdlRedefinirSenha
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Email { get; set; }
    }
}
