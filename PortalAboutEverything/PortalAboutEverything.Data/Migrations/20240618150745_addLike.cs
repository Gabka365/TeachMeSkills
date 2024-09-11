using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalAboutEverything.Data.Migrations
{
    /// <inheritdoc />
    public partial class addLike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Like",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Like", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LikeTraveling",
                columns: table => new
                {
                    LikesId = table.Column<int>(type: "int", nullable: false),
                    TravelingsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeTraveling", x => new { x.LikesId, x.TravelingsId });
                    table.ForeignKey(
                        name: "FK_LikeTraveling_Like_LikesId",
                        column: x => x.LikesId,
                        principalTable: "Like",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeTraveling_Travelings_TravelingsId",
                        column: x => x.TravelingsId,
                        principalTable: "Travelings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikeUser",
                columns: table => new
                {
                    LikesId = table.Column<int>(type: "int", nullable: false),
                    UsersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeUser", x => new { x.LikesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_LikeUser_Like_LikesId",
                        column: x => x.LikesId,
                        principalTable: "Like",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikeTraveling_TravelingsId",
                table: "LikeTraveling",
                column: "TravelingsId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeUser_UsersId",
                table: "LikeUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeTraveling");

            migrationBuilder.DropTable(
                name: "LikeUser");

            migrationBuilder.DropTable(
                name: "Like");
        }
    }
}
