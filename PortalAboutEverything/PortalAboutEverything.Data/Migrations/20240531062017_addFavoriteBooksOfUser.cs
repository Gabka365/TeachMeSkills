using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalAboutEverything.Data.Migrations
{
    /// <inheritdoc />
    public partial class addFavoriteBooksOfUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookUser",
                columns: table => new
                {
                    FavoriteBooksOfUserId = table.Column<int>(type: "int", nullable: false),
                    UsersWhoAddBookToFavoritesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookUser", x => new { x.FavoriteBooksOfUserId, x.UsersWhoAddBookToFavoritesId });
                    table.ForeignKey(
                        name: "FK_BookUser_Books_FavoriteBooksOfUserId",
                        column: x => x.FavoriteBooksOfUserId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookUser_Users_UsersWhoAddBookToFavoritesId",
                        column: x => x.UsersWhoAddBookToFavoritesId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookUser_UsersWhoAddBookToFavoritesId",
                table: "BookUser",
                column: "UsersWhoAddBookToFavoritesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookUser");
        }
    }
}
