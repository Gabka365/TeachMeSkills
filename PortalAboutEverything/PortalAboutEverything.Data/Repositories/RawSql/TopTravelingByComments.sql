SELECT TOP 1 
    t.Id, 
    t.Name, 
    t.[Desc], 
    t.TimeOfCreation, 
    t.UserId,
    COUNT(c.Id) AS CommentCount
FROM 
    Travelings AS t
LEFT JOIN 
    Comments AS c ON t.Id = c.TravelingId
GROUP BY 
    t.Id, t.Name, t.[Desc], t.TimeOfCreation, t.UserId
ORDER BY 
    CommentCount DESC

