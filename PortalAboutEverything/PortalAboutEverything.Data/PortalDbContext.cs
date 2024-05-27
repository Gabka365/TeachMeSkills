﻿using Microsoft.EntityFrameworkCore;
using PortalAboutEverything.Data.Model;
using PortalAboutEverything.Data.Model.Store;
using PortalAboutEverything.Data.Model.VideoLibrary;

namespace PortalAboutEverything.Data
{
    public class PortalDbContext : DbContext
    {
        public const string CONNECTION_STRING = "Data Source=(localdb)\\MSSQLLocalDB;Integrated Security=True;Database=Net16Portal";

        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<GameStore> GameStores { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieReview> MovieReviews { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<BoardGame> BoardGames { get; set; }
        public DbSet<BoardGameReview> BoardGameReviews { get; set; }
        public DbSet<Traveling> Travelings { get; set; }
        public DbSet<History> HistoryEvents { get; set; }
        public DbSet<Video> Videos { get; init; }
        public DbSet<Folder> Folders { get; init; }
        
        public PortalDbContext() { }
        public PortalDbContext(DbContextOptions<PortalDbContext> contextOptions) : base(contextOptions) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(CONNECTION_STRING);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasMany(x => x.Reviews)
                .WithOne(x => x.Game)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            modelBuilder.Entity<BoardGame>()
                .HasMany(x => x.Reviews)
                .WithOne(x => x.BoardGame)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired(false);

            modelBuilder.Entity<Movie>()
                .HasMany(x => x.Reviews)
                .WithOne(x => x.Movie)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);

            modelBuilder.Entity<User>()
                .HasMany(x => x.FavoriteGames)
                .WithMany(x => x.UserWhoFavoriteTheGame);
            
            modelBuilder.Entity<Folder>()
                .HasMany(folder => folder.Videos)
                .WithOne(video => video.Folder)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
