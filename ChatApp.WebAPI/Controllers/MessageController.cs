using AutoMapper;
using ChatApp.BLL.Services;
using ChatApp.BLL.Services.Interfaces;
using ChatApp.DAL.Entities;
using ChatApp.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MessageController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IMessageService _messageService;

        public MessageController(IMapper mapper, IMessageService messageService)
        {
            _mapper = mapper;
            _messageService = messageService;
        }

        [HttpPost("create")]
        public async Task<ActionResult<MessageDTO>> CreateMessage([FromBody] MessageDTO messageDTO)
        {
            var newMessage = _mapper.Map<Message>(messageDTO);
            await _messageService.CreateMessage(newMessage);

            return Ok(newMessage);
        }

        [HttpGet("getall")]
        public async Task<ActionResult<List<MessageDTO>>> GetAllMessages([FromQuery] int chatId)
        {
            var messages = await _messageService.GetAllMessagesAsync(chatId);
            return _mapper.Map<List<MessageDTO>>(messages);
        }

        [HttpPost("getpaginated")]
        public async Task<ActionResult<PaginatedDataDTO<MessageDTO>>> GetPaginatedMessages(TableStateData<MessageSortProperty> tableStateData)
        {
            var messages = await _messageService.GetPaginatedMessagesAsync(tableStateData);
            return _mapper.Map<PaginatedDataDTO<MessageDTO>>(messages);
        }
    }
}
