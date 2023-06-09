﻿namespace ChatApp.DTO
{
    public class UserDTO
    {
        public string Id { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public List<AvatarDTO>? Avatars { get; set; }

        public UserDTO() { }
    }
    public enum UserInfoSortProperty
    {
        None,
        UserName,
        Email,
        LastName,
        FirstName
    }
}
