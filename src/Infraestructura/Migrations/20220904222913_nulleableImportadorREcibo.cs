using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class nulleableImportadorREcibo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recibo_importador_ImportadorId",
                table: "recibo");

            migrationBuilder.AlterColumn<int>(
                name: "ImportadorId",
                table: "recibo",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_recibo_importador_ImportadorId",
                table: "recibo",
                column: "ImportadorId",
                principalTable: "importador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recibo_importador_ImportadorId",
                table: "recibo");

            migrationBuilder.AlterColumn<int>(
                name: "ImportadorId",
                table: "recibo",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_recibo_importador_ImportadorId",
                table: "recibo",
                column: "ImportadorId",
                principalTable: "importador",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
