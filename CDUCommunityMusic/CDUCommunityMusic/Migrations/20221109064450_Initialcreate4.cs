using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CDUCommunityMusic.Migrations
{
    public partial class Initialcreate4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Letter_LetterId",
                table: "Lesson");

            migrationBuilder.AlterColumn<int>(
                name: "LetterId",
                table: "Lesson",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Letter_LetterId",
                table: "Lesson",
                column: "LetterId",
                principalTable: "Letter",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lesson_Letter_LetterId",
                table: "Lesson");

            migrationBuilder.AlterColumn<int>(
                name: "LetterId",
                table: "Lesson",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Lesson_Letter_LetterId",
                table: "Lesson",
                column: "LetterId",
                principalTable: "Letter",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
