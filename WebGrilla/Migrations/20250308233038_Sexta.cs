using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebGrilla.Migrations
{
    public partial class Sexta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TipoDocumento",
                table: "Recursos",
                newName: "IdTipoDocumento");

            migrationBuilder.CreateTable(
                name: "TiposDocumentos",
                columns: table => new
                {
                    IdTipoDocumento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposDocumentos", x => x.IdTipoDocumento);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Recursos_IdTipoDocumento",
                table: "Recursos",
                column: "IdTipoDocumento");

            migrationBuilder.AddForeignKey(
                name: "FK_Recursos_TiposDocumentos_IdTipoDocumento",
                table: "Recursos",
                column: "IdTipoDocumento",
                principalTable: "TiposDocumentos",
                principalColumn: "IdTipoDocumento",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recursos_TiposDocumentos_IdTipoDocumento",
                table: "Recursos");

            migrationBuilder.DropTable(
                name: "TiposDocumentos");

            migrationBuilder.DropIndex(
                name: "IX_Recursos_IdTipoDocumento",
                table: "Recursos");

            migrationBuilder.RenameColumn(
                name: "IdTipoDocumento",
                table: "Recursos",
                newName: "TipoDocumento");
        }
    }
}
