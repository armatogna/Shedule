using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfApp3.Migrations
{
    /// <inheritdoc />
    public partial class InCr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
