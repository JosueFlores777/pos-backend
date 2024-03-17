using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class modificandoRangosDeCobros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.DropColumn(
                name: "Estado",
                table: "RangoCobros");


            migrationBuilder.AlterColumn<int>(
                name: "ValorMinimo",
                table: "RangoCobros",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PorCada",
                table: "RangoCobros",
                nullable: true);


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recibo_Catalogo_EstadoSefinId",
                table: "recibo");

            migrationBuilder.DropColumn(
                name: "PorCada",
                table: "RangoCobros");

            migrationBuilder.AlterColumn<int>(
                name: "EstadoSefinId",
                table: "recibo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "EstadoSefinaId",
                table: "recibo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ValorMinimo",
                table: "RangoCobros",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "RangoCobros",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_recibo_Catalogo_EstadoSefinId",
                table: "recibo",
                column: "EstadoSefinId",
                principalTable: "Catalogo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
