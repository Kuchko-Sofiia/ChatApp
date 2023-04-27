CREATE TABLE [dbo].[ContactRequest]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [FromUserId] NVARCHAR(450) NULL, 
    [ToUserId] NVARCHAR(450) NULL, 
    [StatusId] INT NULL, 
    [RequestTime] DATETIME NULL, 
    CONSTRAINT [FK_ContactRequest_ToAspNetUsers] FOREIGN KEY ([FromUserId]) REFERENCES [AspNetUsers]([Id]), 
    CONSTRAINT [FK_ContactRequest_ToAspNetUsers1] FOREIGN KEY ([ToUserId]) REFERENCES [AspNetUsers]([Id])
)
