using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalAboutEverything.Data.Migrations
{
    /// <inheritdoc />
    public partial class LinkBetweenMoviesAndUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieUser",
                columns: table => new
                {
                    FavoriteMoviesId = table.Column<int>(type: "int", nullable: false),
                    UsersWhoFavoriteTheMovieId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieUser", x => new { x.FavoriteMoviesId, x.UsersWhoFavoriteTheMovieId });
                    table.ForeignKey(
                        name: "FK_MovieUser_Movies_FavoriteMoviesId",
                        column: x => x.FavoriteMoviesId,
                        principalTable: "Movies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieUser_Users_UsersWhoFavoriteTheMovieId",
                        column: x => x.UsersWhoFavoriteTheMovieId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieUser_UsersWhoFavoriteTheMovieId",
                table: "MovieUser",
                column: "UsersWhoFavoriteTheMovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieUser");
        }
    }
}
