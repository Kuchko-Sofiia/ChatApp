CREATE VIEW [dbo].[ChatMembersCount] AS
SELECT c.Id AS ChatId, c.Name AS ChatName, COUNT(cm.UserId) AS MembersCount
FROM dbo.Chats c
LEFT JOIN dbo.ChatMembers cm ON cm.ChatId = c.Id
GROUP BY c.Id, c.Name;