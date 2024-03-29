using System.ComponentModel.DataAnnotations;

namespace ProjetoProdutos.Models
{
    public class mdlAlteracaoSenha
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string SenhaAtual {  get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string NovaSenha { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        [Compare("novaSenha", ErrorMessage = "As senhas não correspondem.")]
        public string SenhaConfirmacao { get; set; }
    }
}
