﻿using ChatApp.DTO;
using ChatApp.BLL.Models;
using ChatApp.BLL.Services.Interfaces;
using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Interfaces;
using ChatApp.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<User> GetUserById(string id)
        {
            var userRepository = _unitOfWork.GetRepository<IUserRepository>();
            
            return await userRepository.GetUserWithAvatarById(id);
        }

        public async Task<PaginatedData<User>> GetUsersAsync(PaginatedDataStateDTO<UserInfoSortProperty> tableState)
        {
            var userRepository = _unitOfWork.GetRepository<IUserRepository>();
            var users = userRepository.GetAll().Include(u => u.Avatars).AsQueryable();
            var usss = users.ToList();

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

        public async Task<bool> EditUser(UserDTO userToEdit)
        {
            var userRepository = _unitOfWork.GetRepository<IUserRepository>();
            var user =  await userRepository.GetById(userToEdit.Id);

            if (user == null)
                return false;

            user.UserName = userToEdit.UserName;
            user.Email = userToEdit.Email;
            user.FirstName = userToEdit.FirstName;
            user.LastName = userToEdit.LastName;
            user.PhoneNumber = userToEdit.PhoneNumber;

            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task AddAvatarAsync(Avatar avatar)
        {
            var avatarRepository = _unitOfWork.GetRepository<IAvatarRepository>();
            avatarRepository.Create(avatar);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RemoveAvatarAsync(int avatarId)
        {
            var avatarRepository = _unitOfWork.GetRepository<IAvatarRepository>();

            var avatarToDelete = await avatarRepository.GetById(avatarId);
            if(avatarToDelete != null)
            {
                avatarRepository.Delete(avatarToDelete);
            } 

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
