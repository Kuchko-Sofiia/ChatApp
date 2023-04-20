using AutoMapper;
using ChatApp.BLL.Mapping;
using ChatApp.DAL.Entities;

namespace ChatApp.BLL.DTO
{
    public class UserInfoDTO
    {
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        //public DateTime DateOfBirth { get; set; }
        //public DateTime LastTimeActive { get; set; }
        public UserInfoDTO() { }
    }
}
