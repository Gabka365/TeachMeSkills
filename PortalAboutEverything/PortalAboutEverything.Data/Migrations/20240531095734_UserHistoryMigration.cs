using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalAboutEverything.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserHistoryMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HistoryEventUser",
                columns: table => new
                {
                    FavoriteHistoryEventsId = table.Column<int>(type: "int", nullable: false),
                    UserWhoFavoriteTheHistoryEventId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEventUser", x => new { x.FavoriteHistoryEventsId, x.UserWhoFavoriteTheHistoryEventId });
                    table.ForeignKey(
                        name: "FK_HistoryEventUser_HistoryEvents_FavoriteHistoryEventsId",
                        column: x => x.FavoriteHistoryEventsId,
                        principalTable: "HistoryEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoryEventUser_Users_UserWhoFavoriteTheHistoryEventId",
                        column: x => x.UserWhoFavoriteTheHistoryEventId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoryEventUser_UserWhoFavoriteTheHistoryEventId",
                table: "HistoryEventUser",
                column: "UserWhoFavoriteTheHistoryEventId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoryEventUser");
        }
    }
}
