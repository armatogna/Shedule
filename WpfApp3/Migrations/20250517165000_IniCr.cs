using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WpfApp3.Migrations
{
    /// <inheritdoc />
    public partial class IniCr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "idL",
                table: "Subjects",
                type: "integer",
                nullable: true,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "idL",
                table: "Subjects");
        }
    }
}
