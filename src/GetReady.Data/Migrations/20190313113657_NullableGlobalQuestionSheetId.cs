using Microsoft.EntityFrameworkCore.Migrations;

namespace GetReady.Data.Migrations
{
    public partial class NullableGlobalQuestionSheetId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GlobalQuestionPackages_QuestionSheets_QuestionSheetId",
                table: "GlobalQuestionPackages");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionSheetId",
                table: "GlobalQuestionPackages",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_GlobalQuestionPackages_QuestionSheets_QuestionSheetId",
                table: "GlobalQuestionPackages",
                column: "QuestionSheetId",
                principalTable: "QuestionSheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GlobalQuestionPackages_QuestionSheets_QuestionSheetId",
                table: "GlobalQuestionPackages");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionSheetId",
                table: "GlobalQuestionPackages",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GlobalQuestionPackages_QuestionSheets_QuestionSheetId",
                table: "GlobalQuestionPackages",
                column: "QuestionSheetId",
                principalTable: "QuestionSheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
