using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projeto_mvc.Migrations
{
    /// <inheritdoc />
    public partial class ProfessoresAjuste : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CursoProfessor_Professor_ProfessorID",
                table: "CursoProfessor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Professor",
                table: "Professor");

            migrationBuilder.RenameTable(
                name: "Professor",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CursoProfessor_ProfessoresAjuste_ProfessorID",
                table: "CursoProfessor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProfessoresAjuste",
                table: "ProfessoresAjuste");

            migrationBuilder.RenameTable(
                name: "ProfessoresAjuste",
                newName: "Professor");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Professor",
                table: "Professor",
                column: "ProfessorID");

            migrationBuilder.AddForeignKey(
                name: "FK_CursoProfessor_Professor_ProfessorID",
                table: "CursoProfessor",
                column: "ProfessorID",
                principalTable: "Professor",
                principalColumn: "ProfessorID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
