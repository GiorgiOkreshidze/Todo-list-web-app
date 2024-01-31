using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable IDE0058 // Expression value is never used

namespace TodoListApp.Services.Database.Migrations
{
    public partial class CorrectComentsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "TodoTaskCommentEntities");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "TodoTaskCommentEntities");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "TodoTaskEntities",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "TodoTaskEntities",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "TodoTaskCommentEntities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "TodoTaskCommentEntities",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }
    }
}
