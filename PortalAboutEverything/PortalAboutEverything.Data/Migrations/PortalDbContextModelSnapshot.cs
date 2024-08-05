﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PortalAboutEverything.Data;

#nullable disable

namespace PortalAboutEverything.Data.Migrations
{
    [DbContext(typeof(PortalDbContext))]
    partial class PortalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BoardGameUser", b =>
                {
                    b.Property<int>("FavoriteBoardsGamesId")
                        .HasColumnType("int");

                    b.Property<int>("UsersWhoFavoriteThisBoardGameId")
                        .HasColumnType("int");

                    b.HasKey("FavoriteBoardsGamesId", "UsersWhoFavoriteThisBoardGameId");

                    b.HasIndex("UsersWhoFavoriteThisBoardGameId");

                    b.ToTable("BoardGameUser");
                });

            modelBuilder.Entity("BookUser", b =>
                {
                    b.Property<int>("FavoriteBooksOfUserId")
                        .HasColumnType("int");

                    b.Property<int>("UsersWhoAddBookToFavoritesId")
                        .HasColumnType("int");

                    b.HasKey("FavoriteBooksOfUserId", "UsersWhoAddBookToFavoritesId");

                    b.HasIndex("UsersWhoAddBookToFavoritesId");

                    b.ToTable("BookUser");
                });

            modelBuilder.Entity("GameStoreUser", b =>
                {
                    b.Property<int>("MyGamesId")
                        .HasColumnType("int");

                    b.Property<int>("UserTheGameId")
                        .HasColumnType("int");

                    b.HasKey("MyGamesId", "UserTheGameId");

                    b.HasIndex("UserTheGameId");

                    b.ToTable("GameStoreUser");
                });

            modelBuilder.Entity("GameUser", b =>
                {
                    b.Property<int>("FavoriteGamesId")
                        .HasColumnType("int");

                    b.Property<int>("UserWhoFavoriteTheGameId")
                        .HasColumnType("int");

                    b.HasKey("FavoriteGamesId", "UserWhoFavoriteTheGameId");

                    b.HasIndex("UserWhoFavoriteTheGameId");

                    b.ToTable("GameUser");
                });

            modelBuilder.Entity("GoodUser", b =>
                {
                    b.Property<int>("FavouriteGoodsId")
                        .HasColumnType("int");

                    b.Property<int>("UsersWhoLikedTheGoodId")
                        .HasColumnType("int");

                    b.HasKey("FavouriteGoodsId", "UsersWhoLikedTheGoodId");

                    b.HasIndex("UsersWhoLikedTheGoodId");

                    b.ToTable("GoodUser");
                });

            modelBuilder.Entity("LikeTraveling", b =>
                {
                    b.Property<int>("LikesId")
                        .HasColumnType("int");

                    b.Property<int>("TravelingsId")
                        .HasColumnType("int");

                    b.HasKey("LikesId", "TravelingsId");

                    b.HasIndex("TravelingsId");

                    b.ToTable("LikeTraveling");
                });

            modelBuilder.Entity("LikeUser", b =>
                {
                    b.Property<int>("LikesId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("LikesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("LikeUser");
                });

            modelBuilder.Entity("MovieUser", b =>
                {
                    b.Property<int>("FavoriteMoviesId")
                        .HasColumnType("int");

                    b.Property<int>("UsersWhoFavoriteTheMovieId")
                        .HasColumnType("int");

                    b.HasKey("FavoriteMoviesId", "UsersWhoFavoriteTheMovieId");

                    b.HasIndex("UsersWhoFavoriteTheMovieId");

                    b.ToTable("MovieUser");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.BoardGame", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Essence")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiniTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<long>("ProductCode")
                        .HasColumnType("bigint");

                    b.Property<string>("Tags")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BoardGames");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.BoardGameReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateOfCreation")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GameId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("BoardGameReviews");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.BookClub.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BookAuthor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SummaryOfBook")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearOfPublication")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.BookClub.BookReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<int>("BookIllustrationsRating")
                        .HasColumnType("int");

                    b.Property<int>("BookPrintRating")
                        .HasColumnType("int");

                    b.Property<int>("BookRating")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("BookReviews");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TravelingId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TravelingId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.CommentBlog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CurrentTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("CommentsBlog");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.Game", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearOfRelease")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.GameStore", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Developer")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GameName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearOfRelease")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("GameStores");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.History", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("YearOfEvent")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("HistoryEvents");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.Like", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.HasKey("Id");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Budget")
                        .HasColumnType("int");

                    b.Property<string>("CountryOfOrigin")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReleaseYear")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("CurrentTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DislikeCount")
                        .HasColumnType("int");

                    b.Property<int?>("LikeCount")
                        .HasColumnType("int");

                    b.Property<string>("Message")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.Store.Good", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Goods");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.Store.GoodReview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("GoodId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserWhoLeavedAReview")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GoodId");

                    b.ToTable("GoodReviews");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.Traveling", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Desc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeOfCreation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Travelings");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Language")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Permission")
                        .HasColumnType("int");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.VideoLibrary.Folder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Folders");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.VideoLibrary.Video", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Duration")
                        .HasColumnType("float");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FolderId")
                        .HasColumnType("int");

                    b.Property<bool>("IsLiked")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("FolderId");

                    b.ToTable("Videos");
                });

            modelBuilder.Entity("PostUser", b =>
                {
                    b.Property<int>("PostsId")
                        .HasColumnType("int");

                    b.Property<int>("UsersId")
                        .HasColumnType("int");

                    b.HasKey("PostsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("PostUser");
                });

            modelBuilder.Entity("BoardGameUser", b =>
                {
                    b.HasOne("PortalAboutEverything.Data.Model.BoardGame", null)
                        .WithMany()
                        .HasForeignKey("FavoriteBoardsGamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PortalAboutEverything.Data.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UsersWhoFavoriteThisBoardGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BookUser", b =>
                {
                    b.HasOne("PortalAboutEverything.Data.Model.BookClub.Book", null)
                        .WithMany()
                        .HasForeignKey("FavoriteBooksOfUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PortalAboutEverything.Data.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UsersWhoAddBookToFavoritesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameStoreUser", b =>
                {
                    b.HasOne("PortalAboutEverything.Data.Model.GameStore", null)
                        .WithMany()
                        .HasForeignKey("MyGamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PortalAboutEverything.Data.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UserTheGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameUser", b =>
                {
                    b.HasOne("PortalAboutEverything.Data.Model.Game", null)
                        .WithMany()
                        .HasForeignKey("FavoriteGamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PortalAboutEverything.Data.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UserWhoFavoriteTheGameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GoodUser", b =>
                {
                    b.HasOne("PortalAboutEverything.Data.Model.Store.Good", null)
                        .WithMany()
                        .HasForeignKey("FavouriteGoodsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PortalAboutEverything.Data.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UsersWhoLikedTheGoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LikeTraveling", b =>
                {
                    b.HasOne("PortalAboutEverything.Data.Model.Like", null)
                        .WithMany()
                        .HasForeignKey("LikesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PortalAboutEverything.Data.Model.Traveling", null)
                        .WithMany()
                        .HasForeignKey("TravelingsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LikeUser", b =>
                {
                    b.HasOne("PortalAboutEverything.Data.Model.Like", null)
                        .WithMany()
                        .HasForeignKey("LikesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PortalAboutEverything.Data.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MovieUser", b =>
                {
                    b.HasOne("PortalAboutEverything.Data.Model.Movie", null)
                        .WithMany()
                        .HasForeignKey("FavoriteMoviesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PortalAboutEverything.Data.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UsersWhoFavoriteTheMovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.BoardGameReview", b =>
                {
                    b.HasOne("PortalAboutEverything.Data.Model.Game", "Game")
                        .WithMany("Reviews")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Game");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.BookClub.BookReview", b =>
                {
                    b.HasOne("PortalAboutEverything.Data.Model.BookClub.Book", "Book")
                        .WithMany("BookReviews")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Book");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.Comment", b =>
                {
                    b.HasOne("PortalAboutEverything.Data.Model.Traveling", "Traveling")
                        .WithMany("Comments")
                        .HasForeignKey("TravelingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Traveling");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.CommentBlog", b =>
                {
                    b.HasOne("PortalAboutEverything.Data.Model.Post", "Post")
                        .WithMany("CommentsBlog")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Post");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.Store.GoodReview", b =>
                {
                    b.HasOne("PortalAboutEverything.Data.Model.Store.Good", "Good")
                        .WithMany("Reviews")
                        .HasForeignKey("GoodId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Good");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.Traveling", b =>
                {
                    b.HasOne("PortalAboutEverything.Data.Model.User", "User")
                        .WithMany("Travelings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.VideoLibrary.Video", b =>
                {
                    b.HasOne("PortalAboutEverything.Data.Model.VideoLibrary.Folder", "Folder")
                        .WithMany("Videos")
                        .HasForeignKey("FolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Folder");
                });

            modelBuilder.Entity("PostUser", b =>
                {
                    b.HasOne("PortalAboutEverything.Data.Model.Post", null)
                        .WithMany()
                        .HasForeignKey("PostsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PortalAboutEverything.Data.Model.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.BookClub.Book", b =>
                {
                    b.Navigation("BookReviews");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.Game", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.Post", b =>
                {
                    b.Navigation("CommentsBlog");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.Store.Good", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.Traveling", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.User", b =>
                {
                    b.Navigation("Travelings");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.VideoLibrary.Folder", b =>
                {
                    b.Navigation("Videos");
                });
#pragma warning restore 612, 618
        }
    }
}
