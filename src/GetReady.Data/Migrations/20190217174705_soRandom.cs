using Microsoft.EntityFrameworkCore.Migrations;

namespace GetReady.Data.Migrations
{
    public partial class soRandom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GlobalQuestionPackage_QuestionSheets_QuestionSheetId",
                table: "GlobalQuestionPackage");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionPackages_QuestionSheets_QuestionSheetId",
                table: "QuestionPackages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionPackages",
                table: "QuestionPackages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GlobalQuestionPackage",
                table: "GlobalQuestionPackage");

            migrationBuilder.RenameTable(
                name: "QuestionPackages",
                newName: "PersonalQuestionPackages");

            migrationBuilder.RenameTable(
                name: "GlobalQuestionPackage",
                newName: "GlobalQuestionPackages");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionPackages_QuestionSheetId",
                table: "PersonalQuestionPackages",
                newName: "IX_PersonalQuestionPackages_QuestionSheetId");

            migrationBuilder.RenameIndex(
                name: "IX_GlobalQuestionPackage_QuestionSheetId",
                table: "GlobalQuestionPackages",
                newName: "IX_GlobalQuestionPackages_QuestionSheetId");

            migrationBuilder.AddColumn<int>(
                name: "Difficulty",
                table: "PersonalQuestionPackages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Difficulty",
                table: "GlobalQuestionPackages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PersonalQuestionPackages",
                table: "PersonalQuestionPackages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GlobalQuestionPackages",
                table: "GlobalQuestionPackages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GlobalQuestionPackages_QuestionSheets_QuestionSheetId",
                table: "GlobalQuestionPackages",
                column: "QuestionSheetId",
                principalTable: "QuestionSheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonalQuestionPackages_QuestionSheets_QuestionSheetId",
                table: "PersonalQuestionPackages",
                column: "QuestionSheetId",
                principalTable: "QuestionSheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GlobalQuestionPackages_QuestionSheets_QuestionSheetId",
                table: "GlobalQuestionPackages");

            migrationBuilder.DropForeignKey(
                name: "FK_PersonalQuestionPackages_QuestionSheets_QuestionSheetId",
                table: "PersonalQuestionPackages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PersonalQuestionPackages",
                table: "PersonalQuestionPackages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GlobalQuestionPackages",
                table: "GlobalQuestionPackages");

            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "PersonalQuestionPackages");

            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "GlobalQuestionPackages");

            migrationBuilder.RenameTable(
                name: "PersonalQuestionPackages",
                newName: "QuestionPackages");

            migrationBuilder.RenameTable(
                name: "GlobalQuestionPackages",
                newName: "GlobalQuestionPackage");

            migrationBuilder.RenameIndex(
                name: "IX_PersonalQuestionPackages_QuestionSheetId",
                table: "QuestionPackages",
                newName: "IX_QuestionPackages_QuestionSheetId");

            migrationBuilder.RenameIndex(
                name: "IX_GlobalQuestionPackages_QuestionSheetId",
                table: "GlobalQuestionPackage",
                newName: "IX_GlobalQuestionPackage_QuestionSheetId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionPackages",
                table: "QuestionPackages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GlobalQuestionPackage",
                table: "GlobalQuestionPackage",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_GlobalQuestionPackage_QuestionSheets_QuestionSheetId",
                table: "GlobalQuestionPackage",
                column: "QuestionSheetId",
                principalTable: "QuestionSheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionPackages_QuestionSheets_QuestionSheetId",
                table: "QuestionPackages",
                column: "QuestionSheetId",
                principalTable: "QuestionSheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
