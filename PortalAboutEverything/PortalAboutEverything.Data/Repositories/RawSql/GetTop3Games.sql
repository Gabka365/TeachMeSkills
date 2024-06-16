SELECT TOP 3 [Name], 
	SUM(CASE WHEN GU.UserWhoFavoriteTheGameId IS NULL 
		THEN 0 
		ELSE 1 
	END) as CountOfUserWhoLikeIt
FROM Games G
	LEFT JOIN GameUser GU ON G.Id = GU.FavoriteGamesId
GROUP BY [Name]
ORDER BY CountOfUserWhoLikeIt DESC

