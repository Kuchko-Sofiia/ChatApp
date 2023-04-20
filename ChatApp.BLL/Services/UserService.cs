using ChatApp.BLL.DTO;
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

        public User GetUserById(int id)
        {
            var userRepository = _unitOfWork.GetRepository<IUserRepository>();
            return userRepository.GetById(id);
        }

        public async Task<PaginatedData<User>> GetUsersAsync(TableStateData tableState)
        {
            var userRepository = _unitOfWork.GetRepository<IUserRepository>();
            var users = userRepository.GetAll();

            return await PaginatedData<User>.GetPaginatedDataAsync(
                source: users,
                pageIndex: tableState.PageIndex,
                pageSize: tableState.PageSize);
        }
    }
}
