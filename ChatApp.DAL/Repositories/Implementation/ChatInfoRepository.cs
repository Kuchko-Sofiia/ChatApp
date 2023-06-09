﻿using ChatApp.DAL.Data;
using ChatApp.DAL.Entities;
using ChatApp.DAL.Repositories.Interfaces;

namespace ChatApp.DAL.Repositories.Realizations
{
    public class ChatInfoRepository : Repository<ChatInfo>, IChatInfoRepository
    {
        public ChatInfoRepository(ChatAppDbContext context) : base(context) { }
    }
}
