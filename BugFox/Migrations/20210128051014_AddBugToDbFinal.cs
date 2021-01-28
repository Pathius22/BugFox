using Microsoft.EntityFrameworkCore.Migrations;

namespace BugFox.Migrations
{
    public partial class AddBugToDbFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "isResolved",
                table: "Bugs",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isResolved",
                table: "Bugs");
        }
    }
}
