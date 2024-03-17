using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class AddingDiscountRecibo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DescuentoId",
                table: "recibo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_recibo_DescuentoId",
                table: "recibo",
                column: "DescuentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_recibo_Catalogo_DescuentoId",
                table: "recibo",
                column: "DescuentoId",
                principalTable: "Catalogo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recibo_Catalogo_DescuentoId",
                table: "recibo");

            migrationBuilder.DropIndex(
                name: "IX_recibo_DescuentoId",
                table: "recibo");

            migrationBuilder.DropColumn(
                name: "DescuentoId",
                table: "recibo");
        }
    }
}
