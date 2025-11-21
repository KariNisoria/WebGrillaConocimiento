using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebGrilla.Migrations
{
    public partial class CorregirEvaluacionGrillaRecurso : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Agregar IdGrilla a la tabla Evaluacion
            migrationBuilder.AddColumn<int>(
                name: "IdGrilla",
                table: "Evaluacion",
                type: "int",
                nullable: false,
                defaultValue: 1); // Valor por defecto temporal

            // Agregar IdGrilla a la tabla Recursos
            migrationBuilder.AddColumn<int>(
                name: "IdGrilla",
                table: "Recursos",
                type: "int",
                nullable: true);

            // Crear índices para las nuevas foreign keys
            migrationBuilder.CreateIndex(
                name: "IX_Evaluacion_IdGrilla",
                table: "Evaluacion",
                column: "IdGrilla");

            migrationBuilder.CreateIndex(
                name: "IX_Recursos_IdGrilla",
                table: "Recursos",
                column: "IdGrilla");

            // Agregar foreign key constraints
            migrationBuilder.AddForeignKey(
                name: "FK_Evaluacion_Grillas_IdGrilla",
                table: "Evaluacion",
                column: "IdGrilla",
                principalTable: "Grillas",
                principalColumn: "IdGrilla",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recursos_Grillas_IdGrilla",
                table: "Recursos",
                column: "IdGrilla",
                principalTable: "Grillas",
                principalColumn: "IdGrilla",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar foreign keys
            migrationBuilder.DropForeignKey(
                name: "FK_Evaluacion_Grillas_IdGrilla",
                table: "Evaluacion");

            migrationBuilder.DropForeignKey(
                name: "FK_Recursos_Grillas_IdGrilla",
                table: "Recursos");

            // Eliminar índices
            migrationBuilder.DropIndex(
                name: "IX_Evaluacion_IdGrilla",
                table: "Evaluacion");

            migrationBuilder.DropIndex(
                name: "IX_Recursos_IdGrilla",
                table: "Recursos");

            // Eliminar columnas
            migrationBuilder.DropColumn(
                name: "IdGrilla",
                table: "Evaluacion");

            migrationBuilder.DropColumn(
                name: "IdGrilla",
                table: "Recursos");
        }
    }
}