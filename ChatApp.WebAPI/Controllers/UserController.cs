using AutoMapper;
using ChatApp.DTO;
using ChatApp.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ChatApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        
        [HttpPost("getallusers")]
        public async Task<ActionResult<PaginatedDataDTO<UserInfoDTO>>> GetAllUsers(TableStateData tableStateData)
        {
            var users = await _userService.GetUsersAsync(tableStateData);

            return _mapper.Map<PaginatedDataDTO<UserInfoDTO>>(users);
        }
    }
}