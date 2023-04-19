using AutoMapper;
using ChatApp.BLL.DTO;
using ChatApp.BLL.Services;
using ChatApp.BLL.Services.Interfaces;
using ChatApp.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
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

        
        [HttpGet("getallusers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();

            var usersDto = _mapper.Map<List<UserInfoDTO>>(users);

            return Ok(usersDto);
        }
    }
}