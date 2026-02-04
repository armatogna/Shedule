using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WpfApp3.Migrations
{
    /// <inheritdoc />
    public partial class Mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_groupSubjects_Subjects_SubjectId",
                table: "groupSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_groupSubjects",
                table: "groupSubjects");

            migrationBuilder.RenameColumn(
                name: "SubjectId",
                table: "groupSubjects",
                newName: "Subjectid");

            migrationBuilder.RenameIndex(
                name: "IX_groupSubjects_SubjectId",
                table: "groupSubjects",
                newName: "IX_groupSubjects_Subjectid");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "groupSubjects",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_groupSubjects",
                table: "groupSubjects",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_groupSubjects_GroupId",
                table: "groupSubjects",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_groupSubjects_Subjects_Subjectid",
                table: "groupSubjects",
                column: "Subjectid",
                principalTable: "Subjects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_groupSubjects_Subjects_Subjectid",
                table: "groupSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_groupSubjects",
                table: "groupSubjects");

            migrationBuilder.DropIndex(
                name: "IX_groupSubjects_GroupId",
                table: "groupSubjects");

            migrationBuilder.RenameColumn(
                name: "Subjectid",
                table: "groupSubjects",
                newName: "SubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_groupSubjects_Subjectid",
                table: "groupSubjects",
                newName: "IX_groupSubjects_SubjectId");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "groupSubjects",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_groupSubjects",
                table: "groupSubjects",
                columns: new[] { "GroupId", "SubjectId" });

            migrationBuilder.AddForeignKey(
                name: "FK_groupSubjects_Subjects_SubjectId",
                table: "groupSubjects",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
