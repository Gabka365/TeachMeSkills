using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalAboutEverything.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLinkBetweenUsersAndGoods : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoodUser",
                columns: table => new
                {
                    FavouriteGoodsId = table.Column<int>(type: "int", nullable: false),
                    UsersWhoLikedTheGoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodUser", x => new { x.FavouriteGoodsId, x.UsersWhoLikedTheGoodId });
                    table.ForeignKey(
                        name: "FK_GoodUser_Goods_FavouriteGoodsId",
                        column: x => x.FavouriteGoodsId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodUser_Users_UsersWhoLikedTheGoodId",
                        column: x => x.UsersWhoLikedTheGoodId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoodUser_UsersWhoLikedTheGoodId",
                table: "GoodUser",
                column: "UsersWhoLikedTheGoodId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodUser");
        }
    }
}
