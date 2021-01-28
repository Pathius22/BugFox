using Microsoft.EntityFrameworkCore.Migrations;

namespace BugFox.Migrations
{
    public partial class AddBugToDbAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Bugs");

            migrationBuilder.DropColumn(
                name: "Severity",
                table: "Bugs");

            migrationBuilder.AlterColumn<int>(
                name: "SubmittedBy",
                table: "Bugs",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssignedTo",
                table: "Bugs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Priority",
                table: "Bugs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Bugs",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedTo",
                table: "Bugs");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Bugs");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Bugs");

            migrationBuilder.AlterColumn<string>(
                name: "SubmittedBy",
                table: "Bugs",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Bugs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Severity",
                table: "Bugs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
