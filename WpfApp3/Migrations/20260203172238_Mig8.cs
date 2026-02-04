using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfApp3.Migrations
{
    /// <inheritdoc />
    public partial class Mig8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_groupSubjects_Groups_GroupId",
                table: "groupSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_groupSubjects_Subjects_Subjectid",
                table: "groupSubjects");

            migrationBuilder.DropIndex(
                name: "IX_groupSubjects_GroupId",
                table: "groupSubjects");

            migrationBuilder.DropIndex(
                name: "IX_groupSubjects_Subjectid",
                table: "groupSubjects");

            migrationBuilder.RenameColumn(
                name: "Subjectid",
                table: "groupSubjects",
                newName: "Subject");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "groupSubjects",
                newName: "Group");

            migrationBuilder.AddColumn<int>(
                name: "GroupsId",
                table: "groupSubjects",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Subjectsid",
                table: "groupSubjects",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_groupSubjects_GroupsId",
                table: "groupSubjects",
                column: "GroupsId");

            migrationBuilder.CreateIndex(
                name: "IX_groupSubjects_Subjectsid",
                table: "groupSubjects",
                column: "Subjectsid");

            migrationBuilder.AddForeignKey(
                name: "FK_groupSubjects_Groups_GroupsId",
                table: "groupSubjects",
                column: "GroupsId",
                principalTable: "Groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_groupSubjects_Subjects_Subjectsid",
                table: "groupSubjects",
                column: "Subjectsid",
                principalTable: "Subjects",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_groupSubjects_Groups_GroupsId",
                table: "groupSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_groupSubjects_Subjects_Subjectsid",
                table: "groupSubjects");

            migrationBuilder.DropIndex(
                name: "IX_groupSubjects_GroupsId",
                table: "groupSubjects");

            migrationBuilder.DropIndex(
                name: "IX_groupSubjects_Subjectsid",
                table: "groupSubjects");

            migrationBuilder.DropColumn(
                name: "GroupsId",
                table: "groupSubjects");

            migrationBuilder.DropColumn(
                name: "Subjectsid",
                table: "groupSubjects");

            migrationBuilder.RenameColumn(
                name: "Subject",
                table: "groupSubjects",
                newName: "Subjectid");

            migrationBuilder.RenameColumn(
                name: "Group",
                table: "groupSubjects",
                newName: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_groupSubjects_GroupId",
                table: "groupSubjects",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_groupSubjects_Subjectid",
                table: "groupSubjects",
                column: "Subjectid");

            migrationBuilder.AddForeignKey(
                name: "FK_groupSubjects_Groups_GroupId",
                table: "groupSubjects",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_groupSubjects_Subjects_Subjectid",
                table: "groupSubjects",
                column: "Subjectid",
                principalTable: "Subjects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
