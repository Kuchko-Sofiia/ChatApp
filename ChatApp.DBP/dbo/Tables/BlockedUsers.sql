CREATE TABLE [dbo].[BlockedUsers]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [UserId] NVARCHAR(450) NULL, 
    [BlockedUserId] NVARCHAR(450) NULL, 
    CONSTRAINT [FK_BlockedUsers_ToTable] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id]), 
    CONSTRAINT [FK_BlockedUsers_ToTable_1] FOREIGN KEY ([BlockedUserId]) REFERENCES [AspNetUsers]([Id])
)
