using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalAboutEverything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLinkBetweenGameAndReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GameId",
                table: "BoardGameReviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BoardGameReviews_GameId",
                table: "BoardGameReviews",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGameReviews_Games_GameId",
                table: "BoardGameReviews",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardGameReviews_Games_GameId",
                table: "BoardGameReviews");

            migrationBuilder.DropIndex(
                name: "IX_BoardGameReviews_GameId",
                table: "BoardGameReviews");

            migrationBuilder.DropColumn(
                name: "GameId",
                table: "BoardGameReviews");
        }
    }
}
