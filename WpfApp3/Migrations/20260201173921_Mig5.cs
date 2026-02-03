using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfApp3.Migrations
{
    /// <inheritdoc />
    public partial class Mig5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Accounts_Accountid",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Accounts_Accountid",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "Accountid",
                table: "Lessons",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_Accountid",
                table: "Lessons",
                newName: "IX_Lessons_AccountId");

            migrationBuilder.RenameColumn(
                name: "Accountid",
                table: "Groups",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_Accountid",
                table: "Groups",
                newName: "IX_Groups_AccountId");

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Lessons",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AccountId",
                table: "Groups",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Accounts_AccountId",
                table: "Groups",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Accounts_AccountId",
                table: "Lessons",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Accounts_AccountId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Accounts_AccountId",
                table: "Lessons");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Lessons",
                newName: "Accountid");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_AccountId",
                table: "Lessons",
                newName: "IX_Lessons_Accountid");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Groups",
                newName: "Accountid");

            migrationBuilder.RenameIndex(
                name: "IX_Groups_AccountId",
                table: "Groups",
                newName: "IX_Groups_Accountid");

            migrationBuilder.AlterColumn<int>(
                name: "Accountid",
                table: "Lessons",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "Accountid",
                table: "Groups",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Accounts_Accountid",
                table: "Groups",
                column: "Accountid",
                principalTable: "Accounts",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Accounts_Accountid",
                table: "Lessons",
                column: "Accountid",
                principalTable: "Accounts",
                principalColumn: "id");
        }
    }
}
