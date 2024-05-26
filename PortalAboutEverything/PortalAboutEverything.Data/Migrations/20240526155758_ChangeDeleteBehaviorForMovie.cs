using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalAboutEverything.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeleteBehaviorForMovie : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieReviews_Movies_MovieId",
                table: "MovieReviews");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieReviews_Movies_MovieId",
                table: "MovieReviews",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieReviews_Movies_MovieId",
                table: "MovieReviews");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieReviews_Movies_MovieId",
                table: "MovieReviews",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");
        }
    }
}
