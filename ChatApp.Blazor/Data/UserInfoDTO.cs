namespace ChatApp.Blazor.Data
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
    }

    public enum UserInfoSortProperty
    {
        UserName,
        Email,
        LastName,
        FirstName
    }
}