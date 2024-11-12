using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivrariaAPI.Migrations
{
    /// <inheritdoc />
    public partial class AtualizarTipoParaRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tipo",
                table: "Utilizadores",
                newName: "Role");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Utilizadores",
                newName: "Tipo");
        }
    }
}
