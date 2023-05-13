CREATE TABLE [dbo].[Messages]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Text] NVARCHAR(MAX) NOT NULL, 
    [ChatId] INT NOT NULL, 
    [SenderId] NVARCHAR(450) NOT NULL, 
    [SentTime] DATETIME NOT NULL, 
    [MessageStatus] INT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Messages_ToChats] FOREIGN KEY ([ChatId]) REFERENCES [Chats]([Id]) ON DELETE CASCADE, 
    CONSTRAINT [FK_Messages_ToAspNetUsers(From)] FOREIGN KEY ([SenderId]) REFERENCES [AspNetUsers]([Id]) ON DELETE CASCADE, 
)
