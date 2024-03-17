using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class cambiandoModeloRecibo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_recibo_Servicio_ServicioId",
                table: "recibo");


            migrationBuilder.DropColumn(
                name: "ServicioId",
                table: "recibo");

            migrationBuilder.AddColumn<int>(
                name: "CantidadServicio",
                table: "recibo",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "detalleRecibo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReciboId = table.Column<int>(nullable: false),
                    ServicioId = table.Column<int>(nullable: false),
                    Monto = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_detalleRecibo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_detalleRecibo_recibo_ReciboId",
                        column: x => x.ReciboId,
                        principalTable: "recibo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_detalleRecibo_Servicio_ServicioId",
                        column: x => x.ServicioId,
                        principalTable: "Servicio",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_detalleRecibo_ReciboId",
                table: "detalleRecibo",
                column: "ReciboId");

            migrationBuilder.CreateIndex(
                name: "IX_detalleRecibo_ServicioId",
                table: "detalleRecibo",
                column: "ServicioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "detalleRecibo");

            migrationBuilder.DropColumn(
                name: "CantidadServicio",
                table: "recibo");

            migrationBuilder.AddColumn<int>(
                name: "ServicioId",
                table: "recibo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_recibo_ServicioId",
                table: "recibo",
                column: "ServicioId");

            migrationBuilder.AddForeignKey(
                name: "FK_recibo_Servicio_ServicioId",
                table: "recibo",
                column: "ServicioId",
                principalTable: "Servicio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
