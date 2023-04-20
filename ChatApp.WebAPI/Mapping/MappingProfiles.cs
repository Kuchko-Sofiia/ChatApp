using AutoMapper;
using ChatApp.DTO;
using ChatApp.DTO.Authentication;
using ChatApp.BLL.Models;
using ChatApp.DAL.Entities;

namespace ChatApp.API.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<PaginatedData<User>, PaginatedDataDTO<UserInfoDTO>>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src));

            CreateMap<LoginDTO, User>()
                .ForMember(user => user.Email,
                    opt => opt.MapFrom(loginDto => loginDto.Email))
                .ForMember(user => user.PasswordHash,
                    opt => opt.MapFrom(loginDto => loginDto.Password)).ReverseMap();

            CreateMap<ChangePasswordDTO, User>()
                .ForMember(user => user.Email,
                    opt => opt.MapFrom(loginDto => loginDto.Email))
                .ForMember(user => user.PasswordHash,
                    opt => opt.MapFrom(loginDto => loginDto.CurrentPassword)).ReverseMap();

            CreateMap<PaginatedData<User>, PaginatedDataDTO<UserInfoDTO>>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src));

            CreateMap<UserInfoDTO, User>()
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
        }
    }
}
