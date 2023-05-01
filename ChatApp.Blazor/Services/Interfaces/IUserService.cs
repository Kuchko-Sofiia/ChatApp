using ChatApp.DTO;

namespace ChatApp.Blazor.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDTO> GetUserByIdAsync(string id);
        public Task<PaginatedDataDTO<UserDTO>> GetUsersAsync(PaginatedDataStateDTO<UserInfoSortProperty> tableState);
        public Task EditUserAsync(UserDTO userInfoDto);
        public Task AddAvatarAsync(AvatarDTO avatar);
    }
}
