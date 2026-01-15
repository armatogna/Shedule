using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfApp3.Migrations
{
    /// <inheritdoc />
    public partial class IniCre : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Weeks",
                table: "Subjects",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weeks",
                table: "Subjects");
        }
    }
}
