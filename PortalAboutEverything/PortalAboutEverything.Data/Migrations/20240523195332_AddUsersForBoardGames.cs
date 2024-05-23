using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalAboutEverything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUsersForBoardGames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BoardGameUser",
                columns: table => new
                {
                    FavoriteBoardsGamesId = table.Column<int>(type: "int", nullable: false),
                    UsersWhoFavoriteThisBoardGameId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGameUser", x => new { x.FavoriteBoardsGamesId, x.UsersWhoFavoriteThisBoardGameId });
                    table.ForeignKey(
                        name: "FK_BoardGameUser_BoardGames_FavoriteBoardsGamesId",
                        column: x => x.FavoriteBoardsGamesId,
                        principalTable: "BoardGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardGameUser_Users_UsersWhoFavoriteThisBoardGameId",
                        column: x => x.UsersWhoFavoriteThisBoardGameId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardGameUser_UsersWhoFavoriteThisBoardGameId",
                table: "BoardGameUser",
                column: "UsersWhoFavoriteThisBoardGameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardGameUser");
        }
    }
}
