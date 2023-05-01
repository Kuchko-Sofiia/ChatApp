using AutoMapper;
using ChatApp.DTO;
using ChatApp.DTO.Authentication;
using ChatApp.BLL.Models;
using ChatApp.DAL.Entities;
using ChatApp.API.Mapping.Converters;

namespace ChatApp.API.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<PaginatedData<User>, PaginatedDataDTO<UserDTO>>()
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(PaginatedData => PaginatedData.PageIndex))
                .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(PaginatedData => PaginatedData.TotalItems))
                .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(PaginatedData => PaginatedData.TotalPages))
                .ForMember(dest => dest.HasPrevious, opt => opt.MapFrom(PaginatedData => PaginatedData.HasPreviousPage))
                .ForMember(dest => dest.HasNext, opt => opt.MapFrom(PaginatedData => PaginatedData.HasNextPage))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src));

            CreateMap<PaginatedData<Chat>, PaginatedDataDTO<ChatDTO>>()
                .ForMember(dest => dest.PageIndex, opt => opt.MapFrom(PaginatedData => PaginatedData.PageIndex))
                .ForMember(dest => dest.TotalItems, opt => opt.MapFrom(PaginatedData => PaginatedData.TotalItems))
                .ForMember(dest => dest.TotalPages, opt => opt.MapFrom(PaginatedData => PaginatedData.TotalPages))
                .ForMember(dest => dest.HasPrevious, opt => opt.MapFrom(PaginatedData => PaginatedData.HasPreviousPage))
                .ForMember(dest => dest.HasNext, opt => opt.MapFrom(PaginatedData => PaginatedData.HasNextPage))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src));

            CreateMap<PaginatedData<ChatInfo>, PaginatedDataDTO<ChatDTO>>()
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
                .ForMember(user => user.PasswordHash, opt => opt.MapFrom(loginDto => loginDto.Password))
                .ForMember(user => user.DateOfBirth, opt => opt.MapFrom(userInfoDto => userInfoDto.DateOfBirth)).ReverseMap();

            CreateMap<ChangePasswordDTO, User>()
                .ForMember(user => user.Email, opt => opt.MapFrom(loginDto => loginDto.Email))
                .ForMember(user => user.PasswordHash, opt => opt.MapFrom(loginDto => loginDto.CurrentPassword)).ReverseMap();

            CreateMap<UserDTO, User>()
                .ForMember(user => user.Id, opt => opt.MapFrom(userInfoDto => userInfoDto.Id))
                .ForMember(user => user.Email, opt => opt.MapFrom(userInfoDto => userInfoDto.Email))
                .ForMember(user => user.UserName, opt => opt.MapFrom(userInfoDto => userInfoDto.UserName))
                .ForMember(user => user.FirstName, opt => opt.MapFrom(userInfoDto => userInfoDto.FirstName))
                .ForMember(user => user.LastName, opt => opt.MapFrom(userInfoDto => userInfoDto.LastName))
                .ForMember(user => user.PhoneNumber, opt => opt.MapFrom(userInfoDto => userInfoDto.PhoneNumber))
                .ForMember(user => user.DateOfBirth, opt => opt.MapFrom(userInfoDto => userInfoDto.DateOfBirth)).ReverseMap();

            CreateMap<ChatDTO, Chat>()
                .ForMember(chat => chat.Id, opt => opt.MapFrom(chatDTO => chatDTO.Id))
                .ForMember(chat => chat.Name, opt => opt.MapFrom(chatDTO => chatDTO.Name))
                .ForMember(chat => chat.Description, opt => opt.MapFrom(chatDTO => chatDTO.Description))
                .ForMember(chat => chat.MembersCount, opt => opt.MapFrom(chatDTO => chatDTO.MembersCount)).ReverseMap();

            CreateMap<ChatDTO, ChatInfo>()
                .ForMember(chat => chat.ChatId, opt => opt.MapFrom(chatDTO => chatDTO.Id))
                .ForMember(chat => chat.MembersCount, opt => opt.MapFrom(chatDTO => chatDTO.MembersCount)).ReverseMap();

            CreateMap<MessageDTO, Message>()
                .ForMember(m => m.Id, opt => opt.MapFrom(messageDTO => messageDTO.Id))
                .ForMember(m => m.Text, opt => opt.MapFrom(messageDTO => messageDTO.Text))
                .ForMember(m => m.ChatId, opt => opt.MapFrom(messageDTO => messageDTO.ChatId))
                .ForMember(m => m.Chat, opt => opt.MapFrom(messageDTO => messageDTO.Chat))
                .ForMember(m => m.SenderId, opt => opt.MapFrom(messageDTO => messageDTO.SenderId))
                .ForMember(m => m.Sender, opt => opt.MapFrom(messageDTO => messageDTO.Sender))
                .ForMember(m => m.SentTime, opt => opt.MapFrom(messageDTO => messageDTO.SentTime)).ReverseMap();

            CreateMap<AvatarDTO, Avatar>()
                .ForMember(a => a.FileName, opt => opt.MapFrom(avatarDTO => avatarDTO.FileName))
                .ForMember(a => a.ContentType, opt => opt.MapFrom(avatarDTO => avatarDTO.ContentType))
                .ForMember(a => a.Content, opt => opt.MapFrom(avatarDTO => avatarDTO.Content))
                .ForMember(a => a.FileName, opt => opt.MapFrom(avatarDTO => avatarDTO.FileName))
                .ForMember(a => a.ChatId, opt => opt.MapFrom(avatarDTO => avatarDTO.ChatId))
                .ForMember(a => a.UserId, opt => opt.MapFrom(avatarDTO => avatarDTO.UserId)).ReverseMap();

        }
    }
}
