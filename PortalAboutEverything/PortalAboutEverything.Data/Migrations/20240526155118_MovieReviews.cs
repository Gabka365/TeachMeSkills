using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalAboutEverything.Data.Migrations
{
    /// <inheritdoc />
    public partial class MovieReviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieReview_Movies_MovieId",
                table: "MovieReview");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieReview",
                table: "MovieReview");

            migrationBuilder.RenameTable(
                name: "MovieReview",
                newName: "MovieReviews");

            migrationBuilder.RenameColumn(
                name: "date",
                table: "MovieReviews",
                newName: "DateOfCreation");

            migrationBuilder.RenameIndex(
                name: "IX_MovieReview_MovieId",
                table: "MovieReviews",
                newName: "IX_MovieReviews_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieReviews",
                table: "MovieReviews",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieReviews_Movies_MovieId",
                table: "MovieReviews",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieReviews_Movies_MovieId",
                table: "MovieReviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieReviews",
                table: "MovieReviews");

            migrationBuilder.RenameTable(
                name: "MovieReviews",
                newName: "MovieReview");

            migrationBuilder.RenameColumn(
                name: "DateOfCreation",
                table: "MovieReview",
                newName: "date");

            migrationBuilder.RenameIndex(
                name: "IX_MovieReviews_MovieId",
                table: "MovieReview",
                newName: "IX_MovieReview_MovieId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieReview",
                table: "MovieReview",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieReview_Movies_MovieId",
                table: "MovieReview",
                column: "MovieId",
                principalTable: "Movies",
                principalColumn: "Id");
        }
    }
}
