using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEcommerce2.Migrations
{
    public partial class AgregarColumIsAdminLoggedIn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdminLoggedIn",
                table: "Usuarios",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdminLoggedIn",
                table: "Usuarios");
        }
    }
}
