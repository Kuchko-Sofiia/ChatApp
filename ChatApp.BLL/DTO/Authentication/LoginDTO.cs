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
                    opt => opt.MapFrom(loginDto => loginDto.Password));
        }
    }
}
