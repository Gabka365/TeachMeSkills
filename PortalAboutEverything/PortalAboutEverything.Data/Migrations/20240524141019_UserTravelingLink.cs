using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalAboutEverything.Data.Migrations
{
    /// <inheritdoc />
    public partial class UserTravelingLink : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Travelings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Travelings_UserId",
                table: "Travelings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Travelings_Users_UserId",
                table: "Travelings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Travelings_Users_UserId",
                table: "Travelings");

            migrationBuilder.DropIndex(
                name: "IX_Travelings_UserId",
                table: "Travelings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Travelings");
        }
    }
}
