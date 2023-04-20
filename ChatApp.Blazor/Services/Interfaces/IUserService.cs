using ChatApp.DTO;

namespace ChatApp.Blazor.Services.Interfaces
{
    public interface IUserService
    {
        public Task<PaginatedDataDTO<UserInfoDTO>> GetUsersAsync(TableStateData tableState);
    }
}
