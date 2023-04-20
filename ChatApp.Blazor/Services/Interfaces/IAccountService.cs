using ChatApp.Blazor.Data.Authentication;

namespace ChatApp.Blazor.Services.Interfaces
{
    public interface IAccountService
    {
        public Task SignIn(SignInDTO registerDto);
        public Task Login(LoginDTO loginDto);
        public Task ChangePassword(ChangePasswordDTO changePasswordDto);
        public Task Logout();
    }
}
