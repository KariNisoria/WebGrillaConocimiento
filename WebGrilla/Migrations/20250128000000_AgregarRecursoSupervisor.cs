using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebGrilla.Migrations
{
    /// <inheritdoc />
    public partial class AgregarRecursoSupervisor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecursosSupervisores",
                columns: table => new
                {
                    IdRecursoSupervisor = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdRecursoSupervisorAsignado = table.Column<int>(type: "int", nullable: false),
                    IdRecursoSupervisado = table.Column<int>(type: "int", nullable: false),
                    FechaAsignacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Activo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    FechaBaja = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Observaciones = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecursosSupervisores", x => x.IdRecursoSupervisor);
                    table.ForeignKey(
                        name: "FK_RecursosSupervisores_Recursos_IdRecursoSupervisorAsignado",
                        column: x => x.IdRecursoSupervisorAsignado,
                        principalTable: "Recursos",
                        principalColumn: "IdRecurso");
                    table.ForeignKey(
                        name: "FK_RecursosSupervisores_Recursos_IdRecursoSupervisado",
                        column: x => x.IdRecursoSupervisado,
                        principalTable: "Recursos",
                        principalColumn: "IdRecurso");
                });

            migrationBuilder.CreateIndex(
                name: "IX_RecursoSupervisor_Unique",
                table: "RecursosSupervisores",
                columns: new[] { "IdRecursoSupervisorAsignado", "IdRecursoSupervisado" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecursosSupervisores_IdRecursoSupervisado",
                table: "RecursosSupervisores",
                column: "IdRecursoSupervisado");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecursosSupervisores");
        }
    }
}