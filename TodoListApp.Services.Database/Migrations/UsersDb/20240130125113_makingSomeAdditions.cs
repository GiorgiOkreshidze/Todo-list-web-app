using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TodoListApp.Services.Database.Migrations.UsersDb
{
    public partial class makingSomeAdditions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserEntityUserRoleEntity",
                columns: table => new
                {
                    UserEntitiesId = table.Column<long>(type: "bigint", nullable: false),
                    UserRoleEntitiesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserEntityUserRoleEntity", x => new { x.UserEntitiesId, x.UserRoleEntitiesId });
                    table.ForeignKey(
                        name: "FK_UserEntityUserRoleEntity_UserEntities_UserEntitiesId",
                        column: x => x.UserEntitiesId,
                        principalTable: "UserEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserEntityUserRoleEntity_UserRoleEntities_UserRoleEntitiesId",
                        column: x => x.UserRoleEntitiesId,
                        principalTable: "UserRoleEntities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserEntityUserRoleEntity_UserRoleEntitiesId",
                table: "UserEntityUserRoleEntity",
                column: "UserRoleEntitiesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserEntityUserRoleEntity");
        }
    }
}
