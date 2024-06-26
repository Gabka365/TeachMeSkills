SELECT G.Id AS GoodId, G.Name AS GoodName, COUNT(GU.UsersWhoLikedTheGoodId) AS CountUsersWhoLikedIt
FROM Goods G
LEFT JOIN GoodUser GU ON G.Id = GU.FavouriteGoodsId
GROUP BY G.Id, G.Name
ORDER BY CountUsersWhoLikedIt DESC