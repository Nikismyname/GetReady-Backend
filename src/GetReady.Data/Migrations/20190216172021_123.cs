using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GetReady.Data.Migrations
{
    public partial class _123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Difficulty",
                table: "QuestionSheets",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<bool>(
                name: "IsGlobal",
                table: "QuestionSheets",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "QuestionSheetId",
                table: "QuestionSheets",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "QuestionSheets",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GlobalQuestionPackage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Question = table.Column<string>(nullable: true),
                    Answer = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    QuestionSheetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalQuestionPackage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GlobalQuestionPackage_QuestionSheets_QuestionSheetId",
                        column: x => x.QuestionSheetId,
                        principalTable: "QuestionSheets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSheets_QuestionSheetId",
                table: "QuestionSheets",
                column: "QuestionSheetId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionSheets_UserId",
                table: "QuestionSheets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_GlobalQuestionPackage_QuestionSheetId",
                table: "GlobalQuestionPackage",
                column: "QuestionSheetId");

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionSheets_QuestionSheets_QuestionSheetId",
                table: "QuestionSheets",
                column: "QuestionSheetId",
                principalTable: "QuestionSheets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionSheets_Users_UserId",
                table: "QuestionSheets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionSheets_QuestionSheets_QuestionSheetId",
                table: "QuestionSheets");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionSheets_Users_UserId",
                table: "QuestionSheets");

            migrationBuilder.DropTable(
                name: "GlobalQuestionPackage");

            migrationBuilder.DropIndex(
                name: "IX_QuestionSheets_QuestionSheetId",
                table: "QuestionSheets");

            migrationBuilder.DropIndex(
                name: "IX_QuestionSheets_UserId",
                table: "QuestionSheets");

            migrationBuilder.DropColumn(
                name: "IsGlobal",
                table: "QuestionSheets");

            migrationBuilder.DropColumn(
                name: "QuestionSheetId",
                table: "QuestionSheets");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "QuestionSheets");

            migrationBuilder.AlterColumn<int>(
                name: "Difficulty",
                table: "QuestionSheets",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
