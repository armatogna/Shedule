using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfApp3.Migrations
{
    /// <inheritdoc />
    public partial class IC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Groups_GroupId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_GroupId",
                table: "Subjects");

            migrationBuilder.AddColumn<int>(
                name: "GroupsId",
                table: "Subjects",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_GroupsId",
                table: "Subjects",
                column: "GroupsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Groups_GroupsId",
                table: "Subjects",
                column: "GroupsId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subjects_Groups_GroupsId",
                table: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_Subjects_GroupsId",
                table: "Subjects");

            migrationBuilder.DropColumn(
                name: "GroupsId",
                table: "Subjects");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_GroupId",
                table: "Subjects",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Subjects_Groups_GroupId",
                table: "Subjects",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
