CREATE TABLE [dbo].[Contacts]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [UserId] NVARCHAR(450) NULL, 
    [ContactUserId] NVARCHAR(450) NULL, 
    CONSTRAINT [FK_Contacts_ToUsers] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id]), 
    CONSTRAINT [FK_Contacts_ToTable] FOREIGN KEY ([ContactUserId]) REFERENCES [AspNetUsers]([Id])
)
