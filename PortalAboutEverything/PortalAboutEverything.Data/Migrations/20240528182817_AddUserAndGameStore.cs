using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalAboutEverything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserAndGameStore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GameStoreUser",
                columns: table => new
                {
                    MyGamesId = table.Column<int>(type: "int", nullable: false),
                    UserTheGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStoreUser", x => new { x.MyGamesId, x.UserTheGameId });
                    table.ForeignKey(
                        name: "FK_GameStoreUser_GameStores_MyGamesId",
                        column: x => x.MyGamesId,
                        principalTable: "GameStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameStoreUser_Users_UserTheGameId",
                        column: x => x.UserTheGameId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameStoreUser_UserTheGameId",
                table: "GameStoreUser",
                column: "UserTheGameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameStoreUser");
        }
    }
}
