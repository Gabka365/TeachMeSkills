SELECT TOP 3[GameName],
	SUM(CASE WHEN GU.UserTheGameId IS NULL
		THEN 0
		ELSE 1
	END) as BuyGames
FROM GameStores G
LEFT JOIN GameStoreUser GU ON G.Id = GU.UserTheGameId
GROUP BY [GameName]
ORDER BY BuyGames DESC