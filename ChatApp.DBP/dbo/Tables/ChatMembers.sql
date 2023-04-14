CREATE TABLE [dbo].[ChatMembers]
(
	[Id] NVARCHAR(450) NOT NULL PRIMARY KEY, 
    [ChatId] NVARCHAR(450) NULL, 
    [UserId] NVARCHAR(450) NULL, 
    CONSTRAINT [FK_ChatMembers_ToTable] FOREIGN KEY ([ChatId]) REFERENCES [Chats]([Id]), 
    CONSTRAINT [FK_ChatMembers_ToTable_1] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id])
)
