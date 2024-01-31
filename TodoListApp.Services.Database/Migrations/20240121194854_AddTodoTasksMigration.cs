using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable IDE0058 // Expression value is never used

namespace TodoListApp.Services.Database.Migrations
{
    public partial class AddTodoTasksMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoTaskEntity_TodoListEntities_todoListEntityId",
                table: "TodoTaskEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoTaskEntity",
                table: "TodoTaskEntity");

            migrationBuilder.RenameTable(
                name: "TodoTaskEntity",
                newName: "TodoTaskEntities");

            migrationBuilder.RenameIndex(
                name: "IX_TodoTaskEntity_todoListEntityId",
                table: "TodoTaskEntities",
                newName: "IX_TodoTaskEntities_todoListEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoTaskEntities",
                table: "TodoTaskEntities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoTaskEntities_TodoListEntities_todoListEntityId",
                table: "TodoTaskEntities",
                column: "todoListEntityId",
                principalTable: "TodoListEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TodoTaskEntities_TodoListEntities_todoListEntityId",
                table: "TodoTaskEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoTaskEntities",
                table: "TodoTaskEntities");

            migrationBuilder.RenameTable(
                name: "TodoTaskEntities",
                newName: "TodoTaskEntity");

            migrationBuilder.RenameIndex(
                name: "IX_TodoTaskEntities_todoListEntityId",
                table: "TodoTaskEntity",
                newName: "IX_TodoTaskEntity_todoListEntityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoTaskEntity",
                table: "TodoTaskEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TodoTaskEntity_TodoListEntities_todoListEntityId",
                table: "TodoTaskEntity",
                column: "todoListEntityId",
                principalTable: "TodoListEntities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
