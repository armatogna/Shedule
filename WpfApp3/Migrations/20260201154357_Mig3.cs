using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfApp3.Migrations
{
    /// <inheritdoc />
    public partial class Mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "numDay",
                table: "Lessons",
                newName: "NumDay");

            migrationBuilder.AlterColumn<string>(
                name: "group",
                table: "Lessons",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Lessons",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Day",
                table: "Lessons",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "Accountid",
                table: "Lessons",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Accounts",
                type: "bytea",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_Accountid",
                table: "Lessons",
                column: "Accountid");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Accounts_Accountid",
                table: "Lessons",
                column: "Accountid",
                principalTable: "Accounts",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Accounts_Accountid",
                table: "Lessons");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_Accountid",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Accountid",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "Data",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "NumDay",
                table: "Lessons",
                newName: "numDay");

            migrationBuilder.AlterColumn<string>(
                name: "group",
                table: "Lessons",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Lessons",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Day",
                table: "Lessons",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
