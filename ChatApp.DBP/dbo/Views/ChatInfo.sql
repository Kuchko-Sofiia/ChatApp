CREATE VIEW [dbo].[ChatInfo] AS
SELECT c.Id AS ChatId, 
	   c.Name AS Name,
	   c.Description AS Description,
	   COUNT(cm.UserId) AS MembersCount
FROM dbo.Chats c
LEFT JOIN dbo.ChatMembers cm ON cm.ChatId = c.Id
GROUP BY c.Id, c.Name, c.Description;