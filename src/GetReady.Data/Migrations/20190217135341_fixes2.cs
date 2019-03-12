using Microsoft.EntityFrameworkCore.Migrations;

namespace GetReady.Data.Migrations
{
    public partial class fixes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "QuestionPackages",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "GlobalQuestionPackage",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "QuestionPackages");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "GlobalQuestionPackage");
        }
    }
}
