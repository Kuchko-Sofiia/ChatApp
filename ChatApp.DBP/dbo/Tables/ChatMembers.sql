CREATE TABLE [dbo].[ChatMembers]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [ChatId] INT NULL, 
    [UserId] NVARCHAR(450) NULL, 
    CONSTRAINT [FK_ChatMembers_ToChats] FOREIGN KEY ([ChatId]) REFERENCES [Chats]([Id]), 
    CONSTRAINT [FK_ChatMembers_ToAspNetUsers] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id])
)
