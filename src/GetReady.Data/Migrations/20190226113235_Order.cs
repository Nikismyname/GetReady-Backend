using Microsoft.EntityFrameworkCore.Migrations;

namespace GetReady.Data.Migrations
{
    public partial class Order : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Column",
                table: "PersonalQuestionPackages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "PersonalQuestionPackages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Column",
                table: "GlobalQuestionPackages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "GlobalQuestionPackages",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Column",
                table: "PersonalQuestionPackages");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "PersonalQuestionPackages");

            migrationBuilder.DropColumn(
                name: "Column",
                table: "GlobalQuestionPackages");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "GlobalQuestionPackages");
        }
    }
}
