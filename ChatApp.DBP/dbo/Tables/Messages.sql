CREATE TABLE [dbo].[Messages]
(
	[Id] NVARCHAR(450) NOT NULL PRIMARY KEY, 
    [MessageText] NVARCHAR(MAX) NULL, 
    [ChatId] NVARCHAR(450) NULL, 
    [FromUserId] NVARCHAR(450) NULL, 
    [ToUserId] NVARCHAR(450) NULL, 
    [ForwardedFromUserId] NVARCHAR(450) NOT NULL, 
    [SentTime] DATETIME NULL, 
    [MessageStatus] INT NULL, 
    CONSTRAINT [FK_Messages_ToTable] FOREIGN KEY ([ChatId]) REFERENCES [Chats]([Id]), 
    CONSTRAINT [FK_Messages_ToTable_1] FOREIGN KEY ([FromUserId]) REFERENCES [AspNetUsers]([Id]), 
    CONSTRAINT [FK_Messages_ToTable_2] FOREIGN KEY ([ToUserId]) REFERENCES [AspNetUsers]([Id]), 
    CONSTRAINT [FK_Messages_ToTable_3] FOREIGN KEY ([ForwardedFromUserId]) REFERENCES [AspNetUsers]([Id])
)
