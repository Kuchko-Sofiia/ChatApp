CREATE TABLE [dbo].[BlockedUsers]
(
	[Id] NVARCHAR(450) NOT NULL PRIMARY KEY, 
    [UserId] NVARCHAR(450) NULL, 
    [BlockedUserId] NVARCHAR(450) NULL, 
    CONSTRAINT [FK_BlockedUsers_ToTable] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id]), 
    CONSTRAINT [FK_BlockedUsers_ToTable_1] FOREIGN KEY ([BlockedUserId]) REFERENCES [AspNetUsers]([Id])
)
