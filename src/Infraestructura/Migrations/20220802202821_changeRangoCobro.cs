using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infraestructura.Migrations
{
    public partial class changeRangoCobro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ValorMaximo",
                table: "RangoCobros",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<int>(
                name: "ValorMaximo",
                table: "RangoCobros",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

        }
    }
}
