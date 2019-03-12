using Microsoft.EntityFrameworkCore.Migrations;

namespace GetReady.Data.Migrations
{
    public partial class Approval : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "GlobalQuestionPackages",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Approved",
                table: "GlobalQuestionPackages");
        }
    }
}
