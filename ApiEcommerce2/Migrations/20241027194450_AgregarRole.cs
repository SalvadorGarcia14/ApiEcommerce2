using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEcommerce2.Migrations
{
    public partial class AgregarRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Rango",
                table: "Usuarios",
                newName: "Role");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Usuarios",
                newName: "Rango");
        }
    }
}
