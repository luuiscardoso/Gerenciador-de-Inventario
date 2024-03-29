using System.ComponentModel.DataAnnotations;

namespace ProjetoProdutos.Models
{
    public class mdlLogin
    {
        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Login {  get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Senha { get; set; }
    }
}
