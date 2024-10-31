using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEcommerce2.Migrations
{
    public partial class AddTotalPagarToOrdenes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesOrden_Productos_ProductoId",
                table: "DetallesOrden");

            migrationBuilder.DropIndex(
                name: "IX_DetallesOrden_ProductoId",
                table: "DetallesOrden");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "DetallesOrden");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Ordenes",
                newName: "UsuarioNombre");

            migrationBuilder.RenameColumn(
                name: "NombreUsuario",
                table: "Ordenes",
                newName: "UsuarioEmail");

            migrationBuilder.RenameColumn(
                name: "EmailUsuario",
                table: "Ordenes",
                newName: "TotalPagar");

            migrationBuilder.AddColumn<string>(
                name: "Categoria",
                table: "DetallesOrden",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "PrecioUnitario",
                table: "DetallesOrden",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ProductoNombre",
                table: "DetallesOrden",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categoria",
                table: "DetallesOrden");

            migrationBuilder.DropColumn(
                name: "PrecioUnitario",
                table: "DetallesOrden");

            migrationBuilder.DropColumn(
                name: "ProductoNombre",
                table: "DetallesOrden");

            migrationBuilder.RenameColumn(
                name: "UsuarioNombre",
                table: "Ordenes",
                newName: "Total");

            migrationBuilder.RenameColumn(
                name: "UsuarioEmail",
                table: "Ordenes",
                newName: "NombreUsuario");

            migrationBuilder.RenameColumn(
                name: "TotalPagar",
                table: "Ordenes",
                newName: "EmailUsuario");

            migrationBuilder.AddColumn<int>(
                name: "ProductoId",
                table: "DetallesOrden",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DetallesOrden_ProductoId",
                table: "DetallesOrden",
                column: "ProductoId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesOrden_Productos_ProductoId",
                table: "DetallesOrden",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
