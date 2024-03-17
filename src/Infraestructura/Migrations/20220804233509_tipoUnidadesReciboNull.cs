using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class tipoUnidadesReciboNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_Catalogo_TipoCobroUnidadesId",
                table: "Servicio");

            migrationBuilder.AlterColumn<int>(
                name: "TipoCobroUnidadesId",
                table: "Servicio",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_Catalogo_TipoCobroUnidadesId",
                table: "Servicio",
                column: "TipoCobroUnidadesId",
                principalTable: "Catalogo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_Catalogo_TipoCobroUnidadesId",
                table: "Servicio");

            migrationBuilder.AlterColumn<int>(
                name: "TipoCobroUnidadesId",
                table: "Servicio",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_Catalogo_TipoCobroUnidadesId",
                table: "Servicio",
                column: "TipoCobroUnidadesId",
                principalTable: "Catalogo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
