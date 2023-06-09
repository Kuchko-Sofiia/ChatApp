﻿namespace ChatApp.DTO.Authentication
{
    public class SignInDTO
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
