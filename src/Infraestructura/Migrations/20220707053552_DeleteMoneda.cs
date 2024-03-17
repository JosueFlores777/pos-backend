using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class DeleteMoneda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_Catalogo_MonedaId",
                table: "Servicio");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_Catalogo_MonedaId",
                table: "Servicio",
                column: "MonedaId",
                principalTable: "Catalogo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
