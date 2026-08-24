using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projeto_mvc.Migrations
{
    /// <inheritdoc />
    public partial class CreateCursoProfessorTable : Migration
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
                newName: "CursoProfessores");

            migrationBuilder.RenameIndex(
                name: "IX_CursoProfessor_ProfessorID",
                table: "CursoProfessores",
                newName: "IX_CursoProfessores_ProfessorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CursoProfessores",
                table: "CursoProfessores",
                columns: new[] { "CursoID", "ProfessorID" });

            migrationBuilder.AddForeignKey(
                name: "FK_CursoProfessores_Cursos_CursoID",
                table: "CursoProfessores",
                column: "CursoID",
                principalTable: "Cursos",
                principalColumn: "CursoID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CursoProfessores_Professores_ProfessorID",
                table: "CursoProfessores",
                column: "ProfessorID",
                principalTable: "Professores",
                principalColumn: "ProfessorID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CursoProfessores_Cursos_CursoID",
                table: "CursoProfessores");

            migrationBuilder.DropForeignKey(
                name: "FK_CursoProfessores_Professores_ProfessorID",
                table: "CursoProfessores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CursoProfessores",
                table: "CursoProfessores");

            migrationBuilder.RenameTable(
                name: "CursoProfessores",
                newName: "CursoProfessor");

            migrationBuilder.RenameIndex(
                name: "IX_CursoProfessores_ProfessorID",
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
