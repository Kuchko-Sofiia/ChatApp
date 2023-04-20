﻿using ChatApp.BLL.DTO;
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

        public async Task<List<User>> GetAllUsersAsync()
        {
            var userRepository = _unitOfWork.GetRepository<IUserRepository>();

            return userRepository.GetAll().ToList();
        }
    }
}
