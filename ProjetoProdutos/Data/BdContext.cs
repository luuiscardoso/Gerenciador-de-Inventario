using Microsoft.EntityFrameworkCore;
using ProjetoProdutos.Models;
using Newtonsoft.Json.Linq;

namespace ProjetoProdutos.Data
{
    public class BdContext : DbContext 
    {
        public BdContext(DbContextOptions<BdContext> options) : base(options) 
        {
        }

        public DbSet<mdlProduto> Produtos { get; set; } 

        public DbSet<mdlUsuario> Usuarios { get; set; }

        
    }
}
