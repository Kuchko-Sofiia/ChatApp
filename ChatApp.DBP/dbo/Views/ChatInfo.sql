--CREATE VIEW [dbo].[ChatInfo] AS
--SELECT c.Id AS ChatId, 
--	   c.Name AS Name,
--	   c.Description AS Description,
--	   COUNT(cm.UserId) AS MembersCount
--FROM dbo.Chats c
--LEFT JOIN dbo.ChatMembers cm ON cm.ChatId = c.Id
--GROUP BY c.Id, c.Name, c.Description;

CREATE VIEW [dbo].[ChatInfo] AS
SELECT c.Id AS ChatId, 
	   c.Name AS Name,
	   c.Description AS Description,
	   COUNT(cm.UserId) AS MembersCount,
	   a.Id AS AvatarId, 
	   a.FileName AS AvatarFileName,
	   a.ContentType AS AvatarContentType,
	   a.Content AS AvatarContent
FROM dbo.Chats c
LEFT JOIN dbo.ChatMembers cm ON cm.ChatId = c.Id
LEFT JOIN (
    SELECT *, ROW_NUMBER() OVER (PARTITION BY ChatId ORDER BY Id DESC) AS AvatarRow
    FROM Avatars
    WHERE ChatId IS NOT NULL
) a ON a.ChatId = c.Id AND a.AvatarRow = 1
GROUP BY c.Id, c.Name, c.Description, a.Id, a.FileName, a.ContentType, a.Content;