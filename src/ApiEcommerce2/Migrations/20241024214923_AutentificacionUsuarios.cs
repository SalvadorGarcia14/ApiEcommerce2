using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiEcommerce2.Migrations
{
    public partial class AutentificacionUsuarios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetalleOrden_Ordenes_OrdenId1",
                table: "DetalleOrden");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetalleOrden",
                table: "DetalleOrden");

            migrationBuilder.DropIndex(
                name: "IX_DetalleOrden_OrdenId1",
                table: "DetalleOrden");

            migrationBuilder.DropColumn(
                name: "OrdenId1",
                table: "DetalleOrden");

            migrationBuilder.DropColumn(
                name: "ProductoId",
                table: "DetalleOrden");

            migrationBuilder.RenameTable(
                name: "DetalleOrden",
                newName: "DetallesOrden");

            migrationBuilder.AlterColumn<int>(
                name: "OrdenId",
                table: "DetallesOrden",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetallesOrden",
                table: "DetallesOrden",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetallesOrden_OrdenId",
                table: "DetallesOrden",
                column: "OrdenId");

            migrationBuilder.AddForeignKey(
                name: "FK_DetallesOrden_Ordenes_OrdenId",
                table: "DetallesOrden",
                column: "OrdenId",
                principalTable: "Ordenes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DetallesOrden_Ordenes_OrdenId",
                table: "DetallesOrden");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DetallesOrden",
                table: "DetallesOrden");

            migrationBuilder.DropIndex(
                name: "IX_DetallesOrden_OrdenId",
                table: "DetallesOrden");

            migrationBuilder.RenameTable(
                name: "DetallesOrden",
                newName: "DetalleOrden");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrdenId",
                table: "DetalleOrden",
                type: "TEXT",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "OrdenId1",
                table: "DetalleOrden",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ProductoId",
                table: "DetalleOrden",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_DetalleOrden",
                table: "DetalleOrden",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_DetalleOrden_OrdenId1",
                table: "DetalleOrden",
                column: "OrdenId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DetalleOrden_Ordenes_OrdenId1",
                table: "DetalleOrden",
                column: "OrdenId1",
                principalTable: "Ordenes",
                principalColumn: "Id");
        }
    }
}
