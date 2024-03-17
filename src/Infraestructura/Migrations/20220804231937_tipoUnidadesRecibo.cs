using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class tipoUnidadesRecibo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RangoCobros_Catalogo_TipoCobroUnidadesId",
                table: "RangoCobros");

            migrationBuilder.DropIndex(
                name: "IX_RangoCobros_TipoCobroUnidadesId",
                table: "RangoCobros");

            migrationBuilder.DropColumn(
                name: "TipoCobroUnidadesId",
                table: "RangoCobros");

            migrationBuilder.AddColumn<int>(
                name: "TipoCobroUnidadesId",
                table: "Servicio",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_TipoCobroUnidadesId",
                table: "Servicio",
                column: "TipoCobroUnidadesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_Catalogo_TipoCobroUnidadesId",
                table: "Servicio",
                column: "TipoCobroUnidadesId",
                principalTable: "Catalogo",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_Catalogo_TipoCobroUnidadesId",
                table: "Servicio");

            migrationBuilder.DropIndex(
                name: "IX_Servicio_TipoCobroUnidadesId",
                table: "Servicio");

            migrationBuilder.DropColumn(
                name: "TipoCobroUnidadesId",
                table: "Servicio");

            migrationBuilder.AddColumn<int>(
                name: "TipoCobroUnidadesId",
                table: "RangoCobros",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_RangoCobros_TipoCobroUnidadesId",
                table: "RangoCobros",
                column: "TipoCobroUnidadesId");

            migrationBuilder.AddForeignKey(
                name: "FK_RangoCobros_Catalogo_TipoCobroUnidadesId",
                table: "RangoCobros",
                column: "TipoCobroUnidadesId",
                principalTable: "Catalogo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
