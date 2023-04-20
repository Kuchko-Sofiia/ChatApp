using ChatApp.Blazor.Data;

namespace ChatApp.Blazor.Services.Interfaces
{
    public interface IUserService
    {
        public Task<PaginatedDataDTO<UserInfoDTO>> GetUsersAsync(TableStateData tableState);
    }
}
