using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfApp3.Migrations
{
    /// <inheritdoc />
    public partial class Mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Subjectsid",
                table: "Lessons",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_Subjectsid",
                table: "Lessons",
                column: "Subjectsid");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Subjects_Subjectsid",
                table: "Lessons",
                column: "Subjectsid",
                principalTable: "Subjects",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Subjects_Subjectsid",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_Subjectsid",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Subjectsid",
                table: "Lessons");
        }
    }
}
