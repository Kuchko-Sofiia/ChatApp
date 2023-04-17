using AutoMapper;
using ChatApp.BLL.Mapping;
using ChatApp.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.BLL.DTO.Authentication
{
    public class SignInDTO : IMapWith<User>
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SignInDTO, User>()
                .ForMember(x => x.Id, x => x.Ignore())
                .ForMember(user => user.UserName,
                    opt => opt.MapFrom(registerDto => registerDto.UserName))
                .ForMember(user => user.Email,
                    opt => opt.MapFrom(registerDto => registerDto.Email))
                .ForMember(user => user.PasswordHash,
                    opt => opt.MapFrom(registerDto => registerDto.Password)).ReverseMap();
        }
    }
}
