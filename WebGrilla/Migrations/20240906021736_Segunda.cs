using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebGrilla.Migrations
{
    public partial class Segunda : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Temas",
                columns: table => new
                {
                    IdTema = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temas", x => x.IdTema);
                });

            migrationBuilder.CreateTable(
                name: "Subtemas",
                columns: table => new
                {
                    IdSubtema = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Orden = table.Column<int>(type: "int", nullable: false),
                    IdTema = table.Column<int>(type: "int", nullable: false),
                    SubtemaIdSubtema = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subtemas", x => x.IdSubtema);
                    table.ForeignKey(
                        name: "FK_Subtemas_Subtemas_SubtemaIdSubtema",
                        column: x => x.SubtemaIdSubtema,
                        principalTable: "Subtemas",
                        principalColumn: "IdSubtema");
                    table.ForeignKey(
                        name: "FK_Subtemas_Temas_IdTema",
                        column: x => x.IdTema,
                        principalTable: "Temas",
                        principalColumn: "IdTema",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Subtemas_IdTema",
                table: "Subtemas",
                column: "IdTema");

            migrationBuilder.CreateIndex(
                name: "IX_Subtemas_SubtemaIdSubtema",
                table: "Subtemas",
                column: "SubtemaIdSubtema");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subtemas");

            migrationBuilder.DropTable(
                name: "Temas");
        }
    }
}
