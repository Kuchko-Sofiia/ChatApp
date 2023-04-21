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
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(PaginatedData => PaginatedData.PageIndex))
                .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(PaginatedData => PaginatedData.TotalItems))
                .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(PaginatedData => PaginatedData.TotalPages))
                .ForMember(dest => dest.HasPrevious, opt => opt.MapFrom(PaginatedData => PaginatedData.HasPreviousPage))
                .ForMember(dest => dest.HasNext, opt => opt.MapFrom(PaginatedData => PaginatedData.HasNextPage))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src));

            CreateMap<LoginDTO, User>()
                .ForMember(user => user.Email, opt => opt.MapFrom(loginDto => loginDto.Email))
                .ForMember(user => user.PasswordHash, opt => opt.MapFrom(loginDto => loginDto.Password)).ReverseMap();

            CreateMap<SignInDTO, User>()
                .ForMember(user => user.Email, opt => opt.MapFrom(loginDto => loginDto.Email))
                .ForMember(user => user.UserName, opt => opt.MapFrom(userInfoDto => userInfoDto.UserName))
                .ForMember(user => user.PasswordHash, opt => opt.MapFrom(loginDto => loginDto.Password)).ReverseMap();

            CreateMap<ChangePasswordDTO, User>()
                .ForMember(user => user.Email, opt => opt.MapFrom(loginDto => loginDto.Email))
                .ForMember(user => user.PasswordHash, opt => opt.MapFrom(loginDto => loginDto.CurrentPassword)).ReverseMap();

            CreateMap<UserInfoDTO, User>()
                .ForMember(user => user.Id, opt => opt.MapFrom(userInfoDto => userInfoDto.Id))
                .ForMember(user => user.Email, opt => opt.MapFrom(userInfoDto => userInfoDto.Email))
                .ForMember(user => user.UserName, opt => opt.MapFrom(userInfoDto => userInfoDto.UserName))
                .ForMember(user => user.FirstName, opt => opt.MapFrom(userInfoDto => userInfoDto.FirstName))
                .ForMember(user => user.LastName, opt => opt.MapFrom(userInfoDto => userInfoDto.LastName))
                .ForMember(user => user.PhoneNumber, opt => opt.MapFrom(userInfoDto => userInfoDto.PhoneNumber)).ReverseMap();
        }
    }
}
