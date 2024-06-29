using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PortalAboutEverything.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReplaceNowAndCurrentTime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Now",
                table: "Posts",
                newName: "CurrentTime");

            migrationBuilder.RenameColumn(
                name: "Now",
                table: "CommentsBlog",
                newName: "CurrentTime");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentTime",
                table: "Posts",
                newName: "Now");

            migrationBuilder.RenameColumn(
                name: "CurrentTime",
                table: "CommentsBlog",
                newName: "Now");
        }
    }
}
