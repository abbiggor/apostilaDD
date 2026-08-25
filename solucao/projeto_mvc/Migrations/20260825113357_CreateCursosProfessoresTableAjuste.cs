using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projeto_mvc.Migrations
{
    /// <inheritdoc />
    public partial class CreateCursosProfessoresTableAjuste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CursoProfessor_Cursos_CursoID",
                table: "CursoProfessor");

            migrationBuilder.DropForeignKey(
                name: "FK_CursoProfessor_Professores_ProfessorID",
                table: "CursoProfessor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CursoProfessor",
                table: "CursoProfessor");

            migrationBuilder.RenameTable(
                name: "CursoProfessor",
                newName: "CursosProfessores");

            migrationBuilder.RenameIndex(
                name: "IX_CursoProfessor_ProfessorID",
                table: "CursosProfessores",
                newName: "IX_CursosProfessores_ProfessorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CursosProfessores",
                table: "CursosProfessores",
                columns: new[] { "CursoID", "ProfessorID" });

            migrationBuilder.AddForeignKey(
                name: "FK_CursosProfessores_Cursos_CursoID",
                table: "CursosProfessores",
                column: "CursoID",
                principalTable: "Cursos",
                principalColumn: "CursoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CursosProfessores_Professores_ProfessorID",
                table: "CursosProfessores",
                column: "ProfessorID",
                principalTable: "Professores",
                principalColumn: "ProfessorID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CursosProfessores_Cursos_CursoID",
                table: "CursosProfessores");

            migrationBuilder.DropForeignKey(
                name: "FK_CursosProfessores_Professores_ProfessorID",
                table: "CursosProfessores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CursosProfessores",
                table: "CursosProfessores");

            migrationBuilder.RenameTable(
                name: "CursosProfessores",
                newName: "CursoProfessor");

            migrationBuilder.RenameIndex(
                name: "IX_CursosProfessores_ProfessorID",
                table: "CursoProfessor",
                newName: "IX_CursoProfessor_ProfessorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CursoProfessor",
                table: "CursoProfessor",
                columns: new[] { "CursoID", "ProfessorID" });

            migrationBuilder.AddForeignKey(
                name: "FK_CursoProfessor_Cursos_CursoID",
                table: "CursoProfessor",
                column: "CursoID",
                principalTable: "Cursos",
                principalColumn: "CursoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CursoProfessor_Professores_ProfessorID",
                table: "CursoProfessor",
                column: "ProfessorID",
                principalTable: "Professores",
                principalColumn: "ProfessorID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
