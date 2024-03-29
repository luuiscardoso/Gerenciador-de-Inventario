using System.ComponentModel.DataAnnotations;

namespace ProjetoProdutos.Models
{
    public class mdlProduto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public int? Qtd {  get; set; }

        [Required(ErrorMessage = "Campo obrigatório.")]
        public string Categoria { get; set; }

        public int? UsuarioID { get; set; }

        public mdlUsuario? Usuario {  get; set; } 
    }
}
