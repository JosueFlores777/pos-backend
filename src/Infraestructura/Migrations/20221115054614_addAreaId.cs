using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class addAreaId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AreaId",
                table: "recibo",
                nullable: false,
                defaultValue: 0);

           

            migrationBuilder.CreateIndex(
                name: "IX_recibo_AreaId",
                table: "recibo",
                column: "AreaId");

         

          
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recibo_Catalogo_AreaId",
                table: "recibo");

            migrationBuilder.DropTable(
                name: "UsuarioArea");

            migrationBuilder.DropIndex(
                name: "IX_recibo_AreaId",
                table: "recibo");

            migrationBuilder.DropColumn(
                name: "AreaId",
                table: "recibo");
        }
    }
}
