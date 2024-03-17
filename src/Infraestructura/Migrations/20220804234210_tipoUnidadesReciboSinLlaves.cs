using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class tipoUnidadesReciboSinLlaves : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_Catalogo_TipoCobroUnidadesId",
                table: "Servicio");

            migrationBuilder.DropIndex(
                name: "IX_Servicio_TipoCobroUnidadesId",
                table: "Servicio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Servicio_TipoCobroUnidadesId",
                table: "Servicio",
                column: "TipoCobroUnidadesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_Catalogo_TipoCobroUnidadesId",
                table: "Servicio",
                column: "TipoCobroUnidadesId",
                principalTable: "Catalogo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
