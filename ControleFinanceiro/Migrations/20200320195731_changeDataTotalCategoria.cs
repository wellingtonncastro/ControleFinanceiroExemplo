using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ControleFinanceiro.Migrations
{
    public partial class changeDataTotalCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mes",
                table: "TotalCategorias");

            migrationBuilder.AddColumn<DateTime>(
                name: "Data",
                table: "TotalCategorias",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "TotalCategorias");

            migrationBuilder.AddColumn<string>(
                name: "Mes",
                table: "TotalCategorias",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
