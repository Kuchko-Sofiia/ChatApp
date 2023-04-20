using ChatApp.DTO;
using ChatApp.BLL.Models;
using ChatApp.DAL.Entities;

namespace ChatApp.BLL.Services.Interfaces
{
    public interface IUserService
    {
        User GetUserById(int id);
        public Task<PaginatedData<User>> GetUsersAsync(TableStateData<UserInfoSortProperty> tableState);
    }
}