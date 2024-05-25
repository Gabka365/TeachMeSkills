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
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiniTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<long>("ProductCode")
                        .HasColumnType("bigint");

                    b.Property<string>("Tags")
                        .IsRequired()
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

                    b.Property<int?>("BoardGameId")
                        .HasColumnType("int");

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

                    b.HasIndex("BoardGameId");

                    b.HasIndex("GameId");

                    b.ToTable("BoardGameReviews");
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
                        .IsRequired()
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

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Now")
                        .HasColumnType("datetime2");

                    b.Property<string>("message")
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

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GoodId")
                        .HasColumnType("int");

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

                    b.Property<string>("NameImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TimeOfCreation")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Travelings");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.VideoInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Duration")
                        .HasColumnType("float");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsLiked")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Videos");
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

            modelBuilder.Entity("PortalAboutEverything.Data.Model.BoardGameReview", b =>
                {
                    b.HasOne("PortalAboutEverything.Data.Model.BoardGame", "BoardGame")
                        .WithMany("Reviews")
                        .HasForeignKey("BoardGameId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("PortalAboutEverything.Data.Model.Game", "Game")
                        .WithMany("Reviews")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("BoardGame");

                    b.Navigation("Game");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.Store.GoodReview", b =>
                {
                    b.HasOne("PortalAboutEverything.Data.Model.Store.Good", "Good")
                        .WithMany("Reviews")
                        .HasForeignKey("GoodId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Good");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.BoardGame", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.Game", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("PortalAboutEverything.Data.Model.Store.Good", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
