using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalAboutEverything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddBoardGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BoardGameId",
                table: "BoardGameReviews",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BoardGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiniTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Essence = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    ProductCode = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGames", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardGameReviews_BoardGameId",
                table: "BoardGameReviews",
                column: "BoardGameId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardGameReviews_BoardGames_BoardGameId",
                table: "BoardGameReviews",
                column: "BoardGameId",
                principalTable: "BoardGames",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardGameReviews_BoardGames_BoardGameId",
                table: "BoardGameReviews");

            migrationBuilder.DropTable(
                name: "BoardGames");

            migrationBuilder.DropIndex(
                name: "IX_BoardGameReviews_BoardGameId",
                table: "BoardGameReviews");

            migrationBuilder.DropColumn(
                name: "BoardGameId",
                table: "BoardGameReviews");
        }
    }
}
