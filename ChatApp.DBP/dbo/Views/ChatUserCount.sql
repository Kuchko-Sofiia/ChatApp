CREATE VIEW [dbo].[ChatUserCount] AS
SELECT c.Id AS ChatId, c.Name AS ChatName, COUNT(cm.UserId) AS UserCount
FROM dbo.Chats c
LEFT JOIN dbo.ChatMembers cm ON cm.ChatId = c.Id
GROUP BY c.Id, c.Name;