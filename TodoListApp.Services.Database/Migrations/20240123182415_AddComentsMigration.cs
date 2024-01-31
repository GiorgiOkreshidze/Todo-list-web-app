using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable IDE0058 // Expression value is never used

namespace TodoListApp.Services.Database.Migrations
{
    public partial class AddComentsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoTaskEntities_TodoListEntities_todoListEntityId",
                table: "TodoTaskEntities");

            migrationBuilder.RenameColumn(
                name: "todoListEntityId",
                table: "TodoTaskEntities",
                newName: "TodoListEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_TodoTaskEntities_todoListEntityId",
                table: "TodoTaskEntities",
                newName: "IX_TodoTaskEntities_TodoListEntityId");

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "TodoTaskEntities",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "TodoTaskCommentEntities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    todoTaskEntityId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoTaskCommentEntities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoTaskCommentEntities_TodoTaskEntities_todoTaskEntityId",
                        column: x => x.todoTaskEntityId,
                        principalTable: "TodoTaskEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoTaskCommentEntities_todoTaskEntityId",
                table: "TodoTaskCommentEntities",
                column: "todoTaskEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoTaskEntities_TodoListEntities_TodoListEntityId",
                table: "TodoTaskEntities",
                column: "TodoListEntityId",
                principalTable: "TodoListEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoTaskEntities_TodoListEntities_TodoListEntityId",
                table: "TodoTaskEntities");

            migrationBuilder.DropTable(
                name: "TodoTaskCommentEntities");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TodoTaskEntities");

            migrationBuilder.RenameColumn(
                name: "TodoListEntityId",
                table: "TodoTaskEntities",
                newName: "todoListEntityId");

            migrationBuilder.RenameIndex(
                name: "IX_TodoTaskEntities_TodoListEntityId",
                table: "TodoTaskEntities",
                newName: "IX_TodoTaskEntities_todoListEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoTaskEntities_TodoListEntities_todoListEntityId",
                table: "TodoTaskEntities",
                column: "todoListEntityId",
                principalTable: "TodoListEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
