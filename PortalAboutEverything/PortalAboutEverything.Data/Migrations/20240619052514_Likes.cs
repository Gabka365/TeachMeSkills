using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalAboutEverything.Data.Migrations
{
    /// <inheritdoc />
    public partial class Likes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikeTraveling_Like_LikesId",
                table: "LikeTraveling");

            migrationBuilder.DropForeignKey(
                name: "FK_LikeUser_Like_LikesId",
                table: "LikeUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Like",
                table: "Like");

            migrationBuilder.RenameTable(
                name: "Like",
                newName: "Likes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Likes",
                table: "Likes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LikeTraveling_Likes_LikesId",
                table: "LikeTraveling",
                column: "LikesId",
                principalTable: "Likes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikeUser_Likes_LikesId",
                table: "LikeUser",
                column: "LikesId",
                principalTable: "Likes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LikeTraveling_Likes_LikesId",
                table: "LikeTraveling");

            migrationBuilder.DropForeignKey(
                name: "FK_LikeUser_Likes_LikesId",
                table: "LikeUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Likes",
                table: "Likes");

            migrationBuilder.RenameTable(
                name: "Likes",
                newName: "Like");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Like",
                table: "Like",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LikeTraveling_Like_LikesId",
                table: "LikeTraveling",
                column: "LikesId",
                principalTable: "Like",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LikeUser_Like_LikesId",
                table: "LikeUser",
                column: "LikesId",
                principalTable: "Like",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
