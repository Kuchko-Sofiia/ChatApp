CREATE VIEW [dbo].[UserInfo] AS
SELECT 
    u.Id,
    u.UserName,
    u.Email,
    u.PhoneNumber,
    u.FirstName,
    u.LastName,
    u.AccessToken,
    u.RefreshToken,
    u.RefreshTokenExpiryTime,
    a.Id as AvatarId,
    a.FileName as AvatarFileName,
    a.ContentType as AvatarContentType,
    a.Content as AvatarContent
FROM [dbo].[AspNetUsers] u
LEFT JOIN [dbo].[Avatars] a ON u.Id = a.UserId;