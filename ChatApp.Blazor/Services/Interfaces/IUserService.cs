using ChatApp.DTO;

namespace ChatApp.Blazor.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserInfoDTO> GetUserByIdAsync(string id);
        public Task<PaginatedDataDTO<UserInfoDTO>> GetUsersAsync(TableStateData<UserInfoSortProperty> tableState);
        public Task EditUserAsync(UserInfoDTO userInfoDto);
    }
}
