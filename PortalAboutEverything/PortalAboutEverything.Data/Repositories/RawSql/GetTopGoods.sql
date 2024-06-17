SELECT Id, 
	SUM (CASE WHEN GU.UsersWhoLikedTheGoodId IS NULL 
	THEN 0 
	ELSE 1 
END) AS CountUserWhoLikedIt
FROM Goods G
	LEFT JOIN GoodUser GU ON G.Id = GU.FavouriteGoodsId
GROUP BY Id
ORDER BY CountUserWhoLikedIt DESC