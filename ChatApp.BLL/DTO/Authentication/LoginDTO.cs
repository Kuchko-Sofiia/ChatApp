using AutoMapper;
using ChatApp.BLL.Mapping;
using ChatApp.DAL.Entities;

namespace ChatApp.BLL.DTO.Authentication
{
    public class LoginDTO : IMapWith<User>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<LoginDTO, User>()
                .ForMember(user => user.Email,
                    opt => opt.MapFrom(loginDto => loginDto.Email))
                .ForMember(user => user.PasswordHash,
                    opt => opt.MapFrom(loginDto => loginDto.Password)).ReverseMap();
        }
    }
}
