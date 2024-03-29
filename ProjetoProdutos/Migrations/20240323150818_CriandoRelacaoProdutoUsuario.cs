using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoProdutos.Migrations
{
    public partial class CriandoRelacaoProdutoUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioID",
                table: "Produtos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_UsuarioID",
                table: "Produtos",
                column: "UsuarioID");

            migrationBuilder.AddForeignKey(
                name: "FK_Produtos_Usuarios_UsuarioID",
                table: "Produtos",
                column: "UsuarioID",
                principalTable: "Usuarios",
                principalColumn: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produtos_Usuarios_UsuarioID",
                table: "Produtos");

            migrationBuilder.DropIndex(
                name: "IX_Produtos_UsuarioID",
                table: "Produtos");

            migrationBuilder.DropColumn(
                name: "UsuarioID",
                table: "Produtos");
        }
    }
}
