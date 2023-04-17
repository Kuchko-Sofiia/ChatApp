using ChatApp.Blazor.Data;

namespace ChatApp.Blazor.Services.Interfaces
{
    public interface IAccountService
    {
        public Task SignIn(SignInModel registerModel);
        public Task Login(LoginModel loginModel);
        public Task ChangePassword(ChangePasswordModel changePasswordModel);
    }
}
