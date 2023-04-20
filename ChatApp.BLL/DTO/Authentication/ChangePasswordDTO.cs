﻿using AutoMapper;
using ChatApp.BLL.Mapping;
using ChatApp.DAL.Entities;

namespace ChatApp.BLL.DTO.Authentication
{
    public class ChangePasswordDTO : IMapWith<User>
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ChangePasswordDTO, User>()
                .ForMember(user => user.Email,
                    opt => opt.MapFrom(loginDto => loginDto.Email))
                .ForMember(user => user.PasswordHash,
                    opt => opt.MapFrom(loginDto => loginDto.CurrentPassword)).ReverseMap();
        }
    }
}
