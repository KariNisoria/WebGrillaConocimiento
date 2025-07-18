using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebGrilla.Migrations
{
    public partial class Cuarta : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evaluacion",
                columns: table => new
                {
                    IdEvaluacion = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluacion", x => x.IdEvaluacion);
                });

            migrationBuilder.CreateTable(
                name: "ConocimientoRecurso",
                columns: table => new
                {
                    IdConocimientoRecurso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorFuncional = table.Column<int>(type: "int", nullable: false),
                    ValorTecnico = table.Column<int>(type: "int", nullable: false),
                    ValorFuncionalVerif = table.Column<int>(type: "int", nullable: false),
                    ValorTecnicoVerif = table.Column<int>(type: "int", nullable: false),
                    IdRecurso = table.Column<int>(type: "int", nullable: false),
                    IdGrilla = table.Column<int>(type: "int", nullable: false),
                    IdSubtema = table.Column<int>(type: "int", nullable: false),
                    IdEvaluacion = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConocimientoRecurso", x => x.IdConocimientoRecurso);
                    table.ForeignKey(
                        name: "FK_ConocimientoRecurso_Evaluacion_IdEvaluacion",
                        column: x => x.IdEvaluacion,
                        principalTable: "Evaluacion",
                        principalColumn: "IdEvaluacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConocimientoRecurso_Grillas_IdGrilla",
                        column: x => x.IdGrilla,
                        principalTable: "Grillas",
                        principalColumn: "IdGrilla",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConocimientoRecurso_Recursos_IdRecurso",
                        column: x => x.IdRecurso,
                        principalTable: "Recursos",
                        principalColumn: "IdRecurso",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConocimientoRecurso_Subtemas_IdSubtema",
                        column: x => x.IdSubtema,
                        principalTable: "Subtemas",
                        principalColumn: "IdSubtema",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResultadoConocimiento",
                columns: table => new
                {
                    IdResultadoConocimiento = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ValorFuncional = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    ValorTecnico = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: false),
                    IdRecurso = table.Column<int>(type: "int", nullable: false),
                    IdGrilla = table.Column<int>(type: "int", nullable: false),
                    IdEvaluacion = table.Column<int>(type: "int", nullable: false),
                    IdSubtema = table.Column<int>(type: "int", nullable: false),
                    Id_Subtema = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResultadoConocimiento", x => x.IdResultadoConocimiento);
                    table.ForeignKey(
                        name: "FK_ResultadoConocimiento_Evaluacion_IdEvaluacion",
                        column: x => x.IdEvaluacion,
                        principalTable: "Evaluacion",
                        principalColumn: "IdEvaluacion",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultadoConocimiento_Grillas_IdGrilla",
                        column: x => x.IdGrilla,
                        principalTable: "Grillas",
                        principalColumn: "IdGrilla",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ResultadoConocimiento_GrillaSubtemas_IdSubtema",
                        column: x => x.IdSubtema,
                        principalTable: "GrillaSubtemas",
                        principalColumn: "IdGrillaSubtema");
                    table.ForeignKey(
                        name: "FK_ResultadoConocimiento_Recursos_IdRecurso",
                        column: x => x.IdRecurso,
                        principalTable: "Recursos",
                        principalColumn: "IdRecurso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConocimientoRecurso_IdEvaluacion",
                table: "ConocimientoRecurso",
                column: "IdEvaluacion");

            migrationBuilder.CreateIndex(
                name: "IX_ConocimientoRecurso_IdGrilla",
                table: "ConocimientoRecurso",
                column: "IdGrilla");

            migrationBuilder.CreateIndex(
                name: "IX_ConocimientoRecurso_IdRecurso",
                table: "ConocimientoRecurso",
                column: "IdRecurso");

            migrationBuilder.CreateIndex(
                name: "IX_ConocimientoRecurso_IdSubtema",
                table: "ConocimientoRecurso",
                column: "IdSubtema");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoConocimiento_IdEvaluacion",
                table: "ResultadoConocimiento",
                column: "IdEvaluacion");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoConocimiento_IdGrilla",
                table: "ResultadoConocimiento",
                column: "IdGrilla");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoConocimiento_IdRecurso",
                table: "ResultadoConocimiento",
                column: "IdRecurso");

            migrationBuilder.CreateIndex(
                name: "IX_ResultadoConocimiento_IdSubtema",
                table: "ResultadoConocimiento",
                column: "IdSubtema");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConocimientoRecurso");

            migrationBuilder.DropTable(
                name: "ResultadoConocimiento");

            migrationBuilder.DropTable(
                name: "Evaluacion");
        }
    }
}
