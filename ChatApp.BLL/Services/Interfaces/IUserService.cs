﻿using ChatApp.DTO;
using ChatApp.BLL.Models;
using ChatApp.DAL.Entities;

namespace ChatApp.BLL.Services.Interfaces
{
    public interface IUserService
    {
        public User GetUserById(string id);
        public Task<PaginatedData<User>> GetUsersAsync(TableStateData<UserInfoSortProperty> tableState);
    }
}