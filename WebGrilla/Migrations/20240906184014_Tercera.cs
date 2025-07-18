using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebGrilla.Migrations
{
    public partial class Tercera : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subtemas_Subtemas_SubtemaIdSubtema",
                table: "Subtemas");

            migrationBuilder.DropForeignKey(
                name: "FK_Subtemas_Temas_IdTema",
                table: "Subtemas");

            migrationBuilder.DropIndex(
                name: "IX_Subtemas_SubtemaIdSubtema",
                table: "Subtemas");

            migrationBuilder.DropColumn(
                name: "SubtemaIdSubtema",
                table: "Subtemas");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Temas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Subtemas",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Grillas",
                columns: table => new
                {
                    IdGrilla = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grillas", x => x.IdGrilla);
                });

            migrationBuilder.CreateTable(
                name: "GrillaTemas",
                columns: table => new
                {
                    IdGrillaTema = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ponderacion = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    IdGrilla = table.Column<int>(type: "int", nullable: false),
                    IdTema = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrillaTemas", x => x.IdGrillaTema);
                    table.ForeignKey(
                        name: "FK_GrillaTemas_Grillas_IdGrilla",
                        column: x => x.IdGrilla,
                        principalTable: "Grillas",
                        principalColumn: "IdGrilla",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrillaTemas_Temas_IdTema",
                        column: x => x.IdTema,
                        principalTable: "Temas",
                        principalColumn: "IdTema",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GrillaSubtemas",
                columns: table => new
                {
                    IdGrillaSubtema = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ponderacion = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    IdGrillaTema = table.Column<int>(type: "int", nullable: false),
                    IdSubtema = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrillaSubtemas", x => x.IdGrillaSubtema);
                    table.ForeignKey(
                        name: "FK_GrillaSubtemas_GrillaTemas_IdGrillaTema",
                        column: x => x.IdGrillaTema,
                        principalTable: "GrillaTemas",
                        principalColumn: "IdGrillaTema",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GrillaSubtemas_Subtemas_IdSubtema",
                        column: x => x.IdSubtema,
                        principalTable: "Subtemas",
                        principalColumn: "IdSubtema",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GrillaSubtemas_IdGrillaTema",
                table: "GrillaSubtemas",
                column: "IdGrillaTema");

            migrationBuilder.CreateIndex(
                name: "IX_GrillaSubtemas_IdSubtema",
                table: "GrillaSubtemas",
                column: "IdSubtema");

            migrationBuilder.CreateIndex(
                name: "IX_GrillaTemas_IdGrilla",
                table: "GrillaTemas",
                column: "IdGrilla");

            migrationBuilder.CreateIndex(
                name: "IX_GrillaTemas_IdTema",
                table: "GrillaTemas",
                column: "IdTema");

            migrationBuilder.AddForeignKey(
                name: "FK_Subtemas_Temas_IdTema",
                table: "Subtemas",
                column: "IdTema",
                principalTable: "Temas",
                principalColumn: "IdTema");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subtemas_Temas_IdTema",
                table: "Subtemas");

            migrationBuilder.DropTable(
                name: "GrillaSubtemas");

            migrationBuilder.DropTable(
                name: "GrillaTemas");

            migrationBuilder.DropTable(
                name: "Grillas");

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Temas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Nombre",
                table: "Subtemas",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<int>(
                name: "SubtemaIdSubtema",
                table: "Subtemas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subtemas_SubtemaIdSubtema",
                table: "Subtemas",
                column: "SubtemaIdSubtema");

            migrationBuilder.AddForeignKey(
                name: "FK_Subtemas_Subtemas_SubtemaIdSubtema",
                table: "Subtemas",
                column: "SubtemaIdSubtema",
                principalTable: "Subtemas",
                principalColumn: "IdSubtema");

            migrationBuilder.AddForeignKey(
                name: "FK_Subtemas_Temas_IdTema",
                table: "Subtemas",
                column: "IdTema",
                principalTable: "Temas",
                principalColumn: "IdTema",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
