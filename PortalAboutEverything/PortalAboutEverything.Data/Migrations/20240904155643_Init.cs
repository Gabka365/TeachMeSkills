using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PortalAboutEverything.Data.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alerts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsNewBoardGameAlert = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alerts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BoardGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    MiniTitle = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Essence = table.Column<string>(type: "text", nullable: true),
                    Tags = table.Column<string>(type: "text", nullable: true),
                    Price = table.Column<double>(type: "double precision", nullable: false),
                    ProductCode = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGames", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BookAuthor = table.Column<string>(type: "text", nullable: false),
                    BookTitle = table.Column<string>(type: "text", nullable: false),
                    SummaryOfBook = table.Column<string>(type: "text", nullable: false),
                    YearOfPublication = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Folders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    YearOfRelease = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GameStores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GameName = table.Column<string>(type: "text", nullable: false),
                    Developer = table.Column<string>(type: "text", nullable: false),
                    YearOfRelease = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HistoryEvents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    YearOfEvent = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Likes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Likes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ReleaseYear = table.Column<int>(type: "integer", nullable: false),
                    Director = table.Column<string>(type: "text", nullable: false),
                    Budget = table.Column<int>(type: "integer", nullable: false),
                    CountryOfOrigin = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Message = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CurrentTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LikeCount = table.Column<int>(type: "integer", nullable: true),
                    DislikeCount = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Language = table.Column<int>(type: "integer", nullable: false),
                    Permission = table.Column<int>(type: "integer", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    BookRating = table.Column<int>(type: "integer", nullable: false),
                    BookPrintRating = table.Column<int>(type: "integer", nullable: false),
                    BookIllustrationsRating = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    BookId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookReviews_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Videos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FilePath = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<double>(type: "double precision", nullable: false),
                    IsLiked = table.Column<bool>(type: "boolean", nullable: false),
                    FolderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Videos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Videos_Folders_FolderId",
                        column: x => x.FolderId,
                        principalTable: "Folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardGameReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Text = table.Column<string>(type: "text", nullable: false),
                    GameId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGameReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BoardGameReviews_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "GoodReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: false),
                    GoodId = table.Column<int>(type: "integer", nullable: true),
                    UserWhoLeavedAReview = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GoodReviews_Goods_GoodId",
                        column: x => x.GoodId,
                        principalTable: "Goods",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CommentsBlog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    CurrentTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PostId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentsBlog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentsBlog_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AlertUser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    AlertId = table.Column<int>(type: "integer", nullable: false),
                    WhenUserSawAlert = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlertUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlertUser_Alerts_AlertId",
                        column: x => x.AlertId,
                        principalTable: "Alerts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlertUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardGameUser",
                columns: table => new
                {
                    FavoriteBoardsGamesId = table.Column<int>(type: "integer", nullable: false),
                    UsersWhoFavoriteThisBoardGameId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGameUser", x => new { x.FavoriteBoardsGamesId, x.UsersWhoFavoriteThisBoardGameId });
                    table.ForeignKey(
                        name: "FK_BoardGameUser_BoardGames_FavoriteBoardsGamesId",
                        column: x => x.FavoriteBoardsGamesId,
                        principalTable: "BoardGames",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardGameUser_Users_UsersWhoFavoriteThisBoardGameId",
                        column: x => x.UsersWhoFavoriteThisBoardGameId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookUser",
                columns: table => new
                {
                    FavoriteBooksOfUserId = table.Column<int>(type: "integer", nullable: false),
                    UsersWhoAddBookToFavoritesId = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "GameStoreUser",
                columns: table => new
                {
                    MyGamesId = table.Column<int>(type: "integer", nullable: false),
                    UserTheGameId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStoreUser", x => new { x.MyGamesId, x.UserTheGameId });
                    table.ForeignKey(
                        name: "FK_GameStoreUser_GameStores_MyGamesId",
                        column: x => x.MyGamesId,
                        principalTable: "GameStores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameStoreUser_Users_UserTheGameId",
                        column: x => x.UserTheGameId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameUser",
                columns: table => new
                {
                    FavoriteGamesId = table.Column<int>(type: "integer", nullable: false),
                    UserWhoFavoriteTheGameId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameUser", x => new { x.FavoriteGamesId, x.UserWhoFavoriteTheGameId });
                    table.ForeignKey(
                        name: "FK_GameUser_Games_FavoriteGamesId",
                        column: x => x.FavoriteGamesId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameUser_Users_UserWhoFavoriteTheGameId",
                        column: x => x.UserWhoFavoriteTheGameId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GoodUser",
                columns: table => new
                {
                    FavouriteGoodsId = table.Column<int>(type: "integer", nullable: false),
                    UsersWhoLikedTheGoodId = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "LikeUser",
                columns: table => new
                {
                    LikesId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeUser", x => new { x.LikesId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_LikeUser_Likes_LikesId",
                        column: x => x.LikesId,
                        principalTable: "Likes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieUser",
                columns: table => new
                {
                    FavoriteMoviesId = table.Column<int>(type: "integer", nullable: false),
                    UsersWhoFavoriteTheMovieId = table.Column<int>(type: "integer", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "PostUser",
                columns: table => new
                {
                    PostsId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostUser", x => new { x.PostsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_PostUser_Posts_PostsId",
                        column: x => x.PostsId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Travelings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Desc = table.Column<string>(type: "text", nullable: false),
                    TimeOfCreation = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Travelings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Travelings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: false),
                    TravelingId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Travelings_TravelingId",
                        column: x => x.TravelingId,
                        principalTable: "Travelings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikeTraveling",
                columns: table => new
                {
                    LikesId = table.Column<int>(type: "integer", nullable: false),
                    TravelingsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeTraveling", x => new { x.LikesId, x.TravelingsId });
                    table.ForeignKey(
                        name: "FK_LikeTraveling_Likes_LikesId",
                        column: x => x.LikesId,
                        principalTable: "Likes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeTraveling_Travelings_TravelingsId",
                        column: x => x.TravelingsId,
                        principalTable: "Travelings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlertUser_AlertId",
                table: "AlertUser",
                column: "AlertId");

            migrationBuilder.CreateIndex(
                name: "IX_AlertUser_UserId",
                table: "AlertUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardGameReviews_GameId",
                table: "BoardGameReviews",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardGameUser_UsersWhoFavoriteThisBoardGameId",
                table: "BoardGameUser",
                column: "UsersWhoFavoriteThisBoardGameId");

            migrationBuilder.CreateIndex(
                name: "IX_BookReviews_BookId",
                table: "BookReviews",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookUser_UsersWhoAddBookToFavoritesId",
                table: "BookUser",
                column: "UsersWhoAddBookToFavoritesId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_TravelingId",
                table: "Comments",
                column: "TravelingId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentsBlog_PostId",
                table: "CommentsBlog",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStoreUser_UserTheGameId",
                table: "GameStoreUser",
                column: "UserTheGameId");

            migrationBuilder.CreateIndex(
                name: "IX_GameUser_UserWhoFavoriteTheGameId",
                table: "GameUser",
                column: "UserWhoFavoriteTheGameId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodReviews_GoodId",
                table: "GoodReviews",
                column: "GoodId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodUser_UsersWhoLikedTheGoodId",
                table: "GoodUser",
                column: "UsersWhoLikedTheGoodId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeTraveling_TravelingsId",
                table: "LikeTraveling",
                column: "TravelingsId");

            migrationBuilder.CreateIndex(
                name: "IX_LikeUser_UsersId",
                table: "LikeUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieUser_UsersWhoFavoriteTheMovieId",
                table: "MovieUser",
                column: "UsersWhoFavoriteTheMovieId");

            migrationBuilder.CreateIndex(
                name: "IX_PostUser_UsersId",
                table: "PostUser",
                column: "UsersId");

            migrationBuilder.CreateIndex(
                name: "IX_Travelings_UserId",
                table: "Travelings",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Videos_FolderId",
                table: "Videos",
                column: "FolderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlertUser");

            migrationBuilder.DropTable(
                name: "BoardGameReviews");

            migrationBuilder.DropTable(
                name: "BoardGameUser");

            migrationBuilder.DropTable(
                name: "BookReviews");

            migrationBuilder.DropTable(
                name: "BookUser");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "CommentsBlog");

            migrationBuilder.DropTable(
                name: "GameStoreUser");

            migrationBuilder.DropTable(
                name: "GameUser");

            migrationBuilder.DropTable(
                name: "GoodReviews");

            migrationBuilder.DropTable(
                name: "GoodUser");

            migrationBuilder.DropTable(
                name: "HistoryEvents");

            migrationBuilder.DropTable(
                name: "LikeTraveling");

            migrationBuilder.DropTable(
                name: "LikeUser");

            migrationBuilder.DropTable(
                name: "MovieUser");

            migrationBuilder.DropTable(
                name: "PostUser");

            migrationBuilder.DropTable(
                name: "Videos");

            migrationBuilder.DropTable(
                name: "Alerts");

            migrationBuilder.DropTable(
                name: "BoardGames");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "GameStores");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "Travelings");

            migrationBuilder.DropTable(
                name: "Likes");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "Folders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
