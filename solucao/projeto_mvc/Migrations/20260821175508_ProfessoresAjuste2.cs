using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projeto_mvc.Migrations
{
    /// <inheritdoc />
    public partial class ProfessoresAjuste2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CursoProfessor_ProfessoresAjuste_ProfessorID",
                table: "CursoProfessor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessoresAjuste",
                table: "ProfessoresAjuste");

            migrationBuilder.RenameTable(
                name: "ProfessoresAjuste",
                newName: "Professores");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Professores",
                table: "Professores",
                column: "ProfessorID");

            migrationBuilder.AddForeignKey(
                name: "FK_CursoProfessor_Professores_ProfessorID",
                table: "CursoProfessor",
                column: "ProfessorID",
                principalTable: "Professores",
                principalColumn: "ProfessorID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CursoProfessor_Professores_ProfessorID",
                table: "CursoProfessor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Professores",
                table: "Professores");

            migrationBuilder.RenameTable(
                name: "Professores",
                newName: "ProfessoresAjuste");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProfessoresAjuste",
                table: "ProfessoresAjuste",
                column: "ProfessorID");

            migrationBuilder.AddForeignKey(
                name: "FK_CursoProfessor_ProfessoresAjuste_ProfessorID",
                table: "CursoProfessor",
                column: "ProfessorID",
                principalTable: "ProfessoresAjuste",
                principalColumn: "ProfessorID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
