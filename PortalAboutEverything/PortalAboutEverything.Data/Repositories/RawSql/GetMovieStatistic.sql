SELECT DISTINCT M.Id, [Name], [Director],
SUM(CASE WHEN MU.UsersWhoFavoriteTheMovieId IS NULL
		THEN 0 
		ELSE 1 
	END) AS CountOfUsersWhoFavorite,
SUM(CASE WHEN MR.Id IS NULL
		THEN 0 
		ELSE 1 
	END) OVER (PARTITION BY Name) AS CountOfMovieReview
FROM Movies M
LEFT JOIN MovieUser MU ON M.Id = MU.FavoriteMoviesId
LEFT JOIN MovieReviews MR ON M.Id = MR.MovieId
GROUP BY M.Id, [Name], [Director], MR.Id
ORDER BY [Name]

