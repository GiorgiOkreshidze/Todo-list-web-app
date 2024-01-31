using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable IDE0058 // Expression value is never used

namespace TodoListApp.Services.Database.Migrations
{
    public partial class AddingTags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagEntityTodoTaskEntity",
                columns: table => new
                {
                    TagEntitiesId = table.Column<long>(type: "bigint", nullable: false),
                    todoTaskEntitiesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagEntityTodoTaskEntity", x => new { x.TagEntitiesId, x.todoTaskEntitiesId });
                    table.ForeignKey(
                        name: "FK_TagEntityTodoTaskEntity_Tags_TagEntitiesId",
                        column: x => x.TagEntitiesId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagEntityTodoTaskEntity_TodoTaskEntities_todoTaskEntitiesId",
                        column: x => x.todoTaskEntitiesId,
                        principalTable: "TodoTaskEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagEntityTodoTaskEntity_todoTaskEntitiesId",
                table: "TagEntityTodoTaskEntity",
                column: "todoTaskEntitiesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagEntityTodoTaskEntity");

            migrationBuilder.DropTable(
                name: "Tags");
        }
    }
}
