﻿using AutoMapper;
using ChatApp.BLL.Services.Interfaces;
using ChatApp.DAL.Entities;
using ChatApp.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IChatService _chatService;

        public ChatController(IMapper mapper, IChatService chatService)
        {
            _mapper = mapper;
            _chatService = chatService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ChatDTO>> CreateChat([FromBody] ChatDTO chatDTO)
        {
            var newChat = _mapper.Map<Chat>(chatDTO);
            await _chatService.CreateChat(newChat, chatDTO.MembersId);

            return Ok(newChat);
        }

        [HttpGet("getbyid")]
        public async Task<ActionResult<ChatDTO>> GetChatById([FromQuery] int chatId)
        {
            var chat = await _chatService.GetChatById(chatId);
            return _mapper.Map<ChatDTO>(chat);
        }

        [HttpPost("getall")]
        public async Task<ActionResult<PaginatedDataDTO<ChatDTO>>> GetAllChats(PaginatedDataStateDTO<ChatSortProperty> tableStateData)
        {
            var chats = await _chatService.GetPaginatedChatsAsync(tableStateData);
            return _mapper.Map<PaginatedDataDTO<ChatDTO>>(chats);
        }

        [HttpPost("getallchatsbyuser/{userId}")]
        public async Task<ActionResult<PaginatedDataDTO<ChatDTO>>> GetAllChatsByUserId(PaginatedDataStateDTO<ChatSortProperty> tableStateData, string userId)
        {
            var chats = await _chatService.GetPaginatedChatsByUserIdAsync(tableStateData, userId);
            return _mapper.Map<PaginatedDataDTO<ChatDTO>>(chats);
        }
    }
}
