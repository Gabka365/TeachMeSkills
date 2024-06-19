﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PortalAboutEverything.Data.Enums;
using PortalAboutEverything.Data.Model;

namespace PortalAboutEverything.Data.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(PortalDbContext dbContext) : base(dbContext) { }

        public bool Exist(string login)
            => _dbSet.Any(x => x.UserName == login);

        public User? GetByLoginAndPasswrod(string login, string password)
        {
            return _dbSet
                .FirstOrDefault(x => x.UserName == login && x.Password == password);
        }

        public Language GetLanguage(int userId)
        {
            return _dbSet
                .Where(x => x.Id == userId)
                .Select(x => x.Language)
                .First();
        }

        public User? GetWithFavoriteBoardGames(int id)
             => _dbSet
            .Include(user => user.FavoriteBoardsGames)
            .Single(user => user.Id == id);

        public void UpdatePermission(int userId, Permission userPermission)
        {
            var user = Get(userId);
            user.Permission = userPermission;
            _dbContext.SaveChanges();
        }

		public void AddMovieToMoviesFan(Movie movie, int userId)
		{
			var user = GetWithFavoriteMovies(userId);
			var movies = user.FavoriteMovies;
			movies.Add(movie);
			user.FavoriteMovies = movies;

			_dbContext.SaveChanges();
		}

        public void DeleteMovieFromMoviesFan(Movie movie, int userId)
        {
            var user = GetWithFavoriteMovies(userId);
            var movies = user.FavoriteMovies;
            movies.Remove(movie);
            user.FavoriteMovies = movies;

            _dbContext.SaveChanges();
        }
        public bool CheckLikeUserOnTravelingPost(int userId, int postId)
        {

            var check = false;
            var post = _dbContext.Travelings
                            .Include(p => p.Likes)
                            .ThenInclude(l => l.Users)
                            .FirstOrDefault(p => p.Id == postId);


            if (post != null)
            {
                var existingLike = post.Likes
                    .FirstOrDefault(like => like.Users.Any(user => user.Id == userId));

                if (existingLike != null)
                {
                    check =  true;
                }
                else
                {
                    check =  false;
                }
            }            
            return check;
        }

        public User? GetWithFavoriteMovies(int id)
			 => _dbSet
			.Include(user => user.FavoriteMovies)
			.Single(user => user.Id == id);
    }
}
