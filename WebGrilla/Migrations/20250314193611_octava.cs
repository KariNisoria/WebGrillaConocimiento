using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebGrilla.Migrations
{
    public partial class octava : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CorreoElectronico",
                table: "Recursos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PerfilSeguridad",
                table: "Recursos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorreoElectronico",
                table: "Recursos");

            migrationBuilder.DropColumn(
                name: "PerfilSeguridad",
                table: "Recursos");
        }
    }
}
