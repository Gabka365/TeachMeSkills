SELECT DISTINCT M.Id, [Name], [Director],
SUM(CASE WHEN MU.UsersWhoFavoriteTheMovieId IS NULL
		THEN 0 
		ELSE 1 
	END) AS CountOfUsersWhoFavorite
FROM Movies M
LEFT JOIN MovieUser MU ON M.Id = MU.FavoriteMoviesId
GROUP BY M.Id, [Name], [Director]
ORDER BY [Name]

