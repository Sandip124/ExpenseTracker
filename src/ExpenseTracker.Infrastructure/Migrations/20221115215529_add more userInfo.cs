using Microsoft.EntityFrameworkCore.Migrations;

namespace ExpenseTracker.Infrastructure.Migrations
{
    public partial class addmoreuserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "user",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "user",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "user");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "user");
        }
    }
}
