using AutoMapper;
using ChatApp.BLL.Mapping;
using ChatApp.DAL.Entities;

namespace ChatApp.BLL.DTO
{
    public class UserInfoDTO : IMapWith<User>
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        //public DateTime DateOfBirth { get; set; }
        //public DateTime LastTimeActive { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UserInfoDTO, User>()
                .ForMember(user => user.Email,
                    opt => opt.MapFrom(userInfoDto => userInfoDto.Email))
                .ForMember(user => user.UserName,
                    opt => opt.MapFrom(userInfoDto => userInfoDto.UserName))
                .ForMember(user => user.FirstName,
                    opt => opt.MapFrom(userInfoDto => userInfoDto.FirstName))
                .ForMember(user => user.LastName,
                    opt => opt.MapFrom(userInfoDto => userInfoDto.LastName))
                .ForMember(user => user.PhoneNumber,
                    opt => opt.MapFrom(userInfoDto => userInfoDto.PhoneNumber)).ReverseMap();
            //.ForMember(user => user.DateOfBirth,
            //    opt => opt.MapFrom(userInfoDto => userInfoDto.DateOfBirth))
            //.ForMember(user => user.LastTimeActive,
            //    opt => opt.MapFrom(userInfoDto => userInfoDto.LastTimeActive)).ReverseMap();
        }
    }
    public enum UserInfoSortProperty
    {
        UserName,
        Email,
        LastName,
        FirstName
    }
}
