using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class delete_subCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Servicio_Catalogo_SubCategoriaId",
                table: "Servicio");

            migrationBuilder.DropIndex(
                name: "IX_Servicio_SubCategoriaId",
                table: "Servicio");

            migrationBuilder.DropColumn(
                name: "SubCategoriaId",
                table: "Servicio");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubCategoriaId",
                table: "Servicio",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Servicio_SubCategoriaId",
                table: "Servicio",
                column: "SubCategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Servicio_Catalogo_SubCategoriaId",
                table: "Servicio",
                column: "SubCategoriaId",
                principalTable: "Catalogo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
