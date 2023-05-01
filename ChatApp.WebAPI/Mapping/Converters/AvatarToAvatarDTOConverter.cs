using AutoMapper;
using ChatApp.DAL.Entities;
using ChatApp.DTO;
using System.Text;

namespace ChatApp.API.Mapping.Converters
{
    public class AvatarToAvatarDTOConverter : ITypeConverter<Avatar, AvatarDTO>
    {
        public AvatarDTO Convert(Avatar source, AvatarDTO destination, ResolutionContext context)
        {
            var avatarDto = new AvatarDTO
            {
                FileName = source.FileName,
                ContentType = source.ContentType,
                Content = Encoding.UTF8.GetBytes(source.Content).ToString(),
                UserId = source.UserId,
                ChatId = source.ChatId
            };

            return avatarDto;
        }
    }
}
