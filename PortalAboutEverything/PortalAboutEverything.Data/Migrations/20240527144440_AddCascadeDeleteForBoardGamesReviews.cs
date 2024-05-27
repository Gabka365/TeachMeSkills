using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalAboutEverything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddCascadeDeleteForBoardGamesReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardGameReviews_BoardGames_BoardGameId",
                table: "BoardGameReviews");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGameReviews_BoardGames_BoardGameId",
                table: "BoardGameReviews",
                column: "BoardGameId",
                principalTable: "BoardGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardGameReviews_BoardGames_BoardGameId",
                table: "BoardGameReviews");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGameReviews_BoardGames_BoardGameId",
                table: "BoardGameReviews",
                column: "BoardGameId",
                principalTable: "BoardGames",
                principalColumn: "Id");
        }
    }
}
