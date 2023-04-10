using AutoMapper;
using ChatApp.BLL.DTO;
using ChatApp.DAL.Entities;

namespace ChatApp.BLL.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
