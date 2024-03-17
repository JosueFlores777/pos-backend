using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class TablaServicio : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AliasServicio",
                table: "Servicio");

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "Servicio",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_DepartamentoId",
                table: "Servicio",
                column: "DepartamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_Catalogo_DepartamentoId",
                table: "Servicio",
                column: "DepartamentoId",
                principalTable: "Catalogo",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_Catalogo_DepartamentoId",
                table: "Servicio");

            migrationBuilder.DropIndex(
                name: "IX_Servicio_DepartamentoId",
                table: "Servicio");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Servicio");

            migrationBuilder.AddColumn<string>(
                name: "AliasServicio",
                table: "Servicio",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
