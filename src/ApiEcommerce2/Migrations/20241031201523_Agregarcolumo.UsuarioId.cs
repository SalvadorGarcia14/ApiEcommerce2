using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEcommerce2.Migrations
{
    public partial class AgregarcolumoUsuarioId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "DetallesOrden");

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Ordenes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Ordenes");

            migrationBuilder.AddColumn<decimal>(
                name: "Subtotal",
                table: "DetallesOrden",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
