using Microsoft.EntityFrameworkCore.Migrations;

namespace GetReady.Data.Migrations
{
    public partial class copyCat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastTenScores",
                table: "PersonalQuestionPackages",
                newName: "LatestScores");

            migrationBuilder.AddColumn<int>(
                name: "DerivedFromId",
                table: "PersonalQuestionPackages",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DerivedFromId",
                table: "GlobalQuestionPackages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DerivedFromId",
                table: "PersonalQuestionPackages");

            migrationBuilder.DropColumn(
                name: "DerivedFromId",
                table: "GlobalQuestionPackages");

            migrationBuilder.RenameColumn(
                name: "LatestScores",
                table: "PersonalQuestionPackages",
                newName: "LastTenScores");
        }
    }
}
