using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalAboutEverything.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveBoardGamesReviewsToMinimalApi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardGameReviews_BoardGames_BoardGameId",
                table: "BoardGameReviews");

            migrationBuilder.DropIndex(
                name: "IX_BoardGameReviews_BoardGameId",
                table: "BoardGameReviews");

            migrationBuilder.DropColumn(
                name: "BoardGameId",
                table: "BoardGameReviews");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoardGameId",
                table: "BoardGameReviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BoardGameReviews_BoardGameId",
                table: "BoardGameReviews",
                column: "BoardGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGameReviews_BoardGames_BoardGameId",
                table: "BoardGameReviews",
                column: "BoardGameId",
                principalTable: "BoardGames",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
