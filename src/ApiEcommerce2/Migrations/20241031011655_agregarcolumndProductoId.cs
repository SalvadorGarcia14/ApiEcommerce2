using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEcommerce2.Migrations
{
    public partial class agregarcolumndProductoId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "DetallesOrden",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Subtotal",
                table: "DetallesOrden",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "DetallesOrden");

            migrationBuilder.DropColumn(
                name: "Subtotal",
                table: "DetallesOrden");
        }
    }
}
