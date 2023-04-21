using ChatApp.DTO;
using ChatApp.BLL.Models;
using ChatApp.BLL.Services.Interfaces;
using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Interfaces;
using ChatApp.DAL.UnitOfWork;

namespace ChatApp.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User GetUserById(string id)
        {
            var userRepository = _unitOfWork.GetRepository<IUserRepository>();
            return userRepository.GetById(id);
        }

        public async Task<PaginatedData<User>> GetUsersAsync(TableStateData<UserInfoSortProperty> tableState)
        {
            var userRepository = _unitOfWork.GetRepository<IUserRepository>();
            var users = userRepository.GetAll();

            if (!String.IsNullOrWhiteSpace(tableState.SearchText))
            {
                string searchText = tableState.SearchText.ToLower();
                users = users.Where(u =>
                    u.Email.ToLower().Contains(searchText) ||
                    u.UserName.ToLower().Contains(searchText) ||
                    u.FirstName.ToLower().Contains(searchText) ||
                    u.LastName.ToLower().Contains(searchText) ||
                    u.PhoneNumber.ToLower().Contains(searchText));
            }

            users = tableState.SortProperty switch
            {
                UserInfoSortProperty.None => users,
                UserInfoSortProperty.UserName => (tableState.SortDirection == SortDirectionData.Ascending)
                    ? users.OrderBy(u => u.UserName)
                    : users.OrderByDescending(u => u.UserName),
                UserInfoSortProperty.Email => (tableState.SortDirection == SortDirectionData.Ascending)
                    ? users.OrderBy(u => u.Email)
                    : users.OrderByDescending(u => u.Email),
                UserInfoSortProperty.FirstName => (tableState.SortDirection == SortDirectionData.Ascending)
                    ? users.OrderBy(u => u.FirstName).ThenBy(u => u.LastName)
                    : users.OrderByDescending(u => u.FirstName).ThenByDescending(u => u.LastName),
                UserInfoSortProperty.LastName => (tableState.SortDirection == SortDirectionData.Ascending)
                    ? users.OrderBy(u => u.LastName).ThenBy(u => u.FirstName)
                    : users.OrderByDescending(u => u.LastName).ThenByDescending(u => u.FirstName),
                _ => users
            };

            return await PaginatedData<User>.GetPaginatedDataAsync(
                source: users,
                pageIndex: tableState.PageIndex,
                pageSize: tableState.PageSize);
        }
    }
}
