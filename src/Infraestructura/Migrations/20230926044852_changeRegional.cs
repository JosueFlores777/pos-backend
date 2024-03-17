using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class changeRegional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recibo_Catalogo_RegionalId",
                table: "recibo");

            migrationBuilder.AlterColumn<int>(
                name: "RegionalId",
                table: "recibo",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_recibo_Catalogo_RegionalId",
                table: "recibo",
                column: "RegionalId",
                principalTable: "Catalogo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recibo_Catalogo_RegionalId",
                table: "recibo");

            migrationBuilder.AlterColumn<int>(
                name: "RegionalId",
                table: "recibo",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_recibo_Catalogo_RegionalId",
                table: "recibo",
                column: "RegionalId",
                principalTable: "Catalogo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
