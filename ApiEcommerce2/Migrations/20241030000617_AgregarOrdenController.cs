using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEcommerce2.Migrations
{
    public partial class AgregarOrdenController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCuenta",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "NombreProducto",
                table: "DetallesOrden");

            migrationBuilder.DropColumn(
                name: "PrecioUnitario",
                table: "DetallesOrden");

            migrationBuilder.RenameColumn(
                name: "Fecha",
                table: "Ordenes",
                newName: "Total");

            migrationBuilder.AddColumn<string>(
                name: "EmailUsuario",
                table: "Ordenes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NombreUsuario",
                table: "Ordenes",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesOrden_Productos_ProductoId",
                table: "DetallesOrden");

            migrationBuilder.DropIndex(
                name: "IX_DetallesOrden_ProductoId",
                table: "DetallesOrden");

            migrationBuilder.DropColumn(
                name: "EmailUsuario",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "NombreUsuario",
                table: "Ordenes");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "DetallesOrden");

            migrationBuilder.RenameColumn(
                name: "Total",
                table: "Ordenes",
                newName: "Fecha");

            migrationBuilder.AddColumn<double>(
                name: "TotalCuenta",
                table: "Ordenes",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Ordenes",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "NombreProducto",
                table: "DetallesOrden",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "PrecioUnitario",
                table: "DetallesOrden",
                type: "REAL",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
