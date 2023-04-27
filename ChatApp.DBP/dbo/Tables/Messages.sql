CREATE TABLE [dbo].[Messages]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [MessageText] NVARCHAR(MAX) NULL, 
    [ChatId] INT NULL, 
    [FromUserId] NVARCHAR(450) NULL, 
    [ToUserId] NVARCHAR(450) NULL, 
    [ForwardedFromUserId] NVARCHAR(450) NOT NULL, 
    [SentTime] DATETIME NULL, 
    [MessageStatus] INT NULL, 
    CONSTRAINT [FK_Messages_ToChats] FOREIGN KEY ([ChatId]) REFERENCES [Chats]([Id]), 
    CONSTRAINT [FK_Messages_ToAspNetUsers(From)] FOREIGN KEY ([FromUserId]) REFERENCES [AspNetUsers]([Id]), 
    CONSTRAINT [FK_Messages_ToAspNetUsers(To)] FOREIGN KEY ([ToUserId]) REFERENCES [AspNetUsers]([Id]), 
    CONSTRAINT [FK_Messages_ToAspNetUsers(Forwarded)] FOREIGN KEY ([ForwardedFromUserId]) REFERENCES [AspNetUsers]([Id])
)
