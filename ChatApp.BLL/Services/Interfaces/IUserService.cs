using ChatApp.DTO;
using ChatApp.BLL.Models;
using ChatApp.DAL.Entities;

namespace ChatApp.BLL.Services.Interfaces
{
    public interface IUserService
    {
        public Task<User> GetUserById(string id);
        public Task<PaginatedData<User>> GetUsersAsync(TableStateData<UserInfoSortProperty> tableState);
        public Task<bool> EditUser(UserInfoDTO userToEdit);
    }
}