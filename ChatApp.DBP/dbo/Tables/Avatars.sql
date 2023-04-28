CREATE TABLE Avatars (
    Id INT PRIMARY KEY,
    FileName VARCHAR(255) NOT NULL,
    ContentType VARCHAR(50) NULL,
    Content VARBINARY(MAX) NOT NULL,
    UserId NVARCHAR(450) NULL,
    ChatId INT NULL, 
    CONSTRAINT [FK_Avatars_ToUsers] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers]([Id]),
    CONSTRAINT [FK_Avatars_ToChats] FOREIGN KEY ([ChatId]) REFERENCES [Chats]([Id]),
    CONSTRAINT CHK_Avatar_FK CHECK (UserId IS NOT NULL OR ChatId IS NOT NULL)
)
