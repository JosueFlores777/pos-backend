using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class addBaseInRangoCobros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BasePeso",
                table: "RangoCobros",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "BaseTarifa",
                table: "RangoCobros",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasePeso",
                table: "RangoCobros");

            migrationBuilder.DropColumn(
                name: "BaseTarifa",
                table: "RangoCobros");
        }
    }
}
