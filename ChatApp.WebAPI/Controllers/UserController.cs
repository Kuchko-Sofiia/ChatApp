using AutoMapper;
using ChatApp.DTO;
using ChatApp.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ChatApp.API.Controllers
{
    [Authorize]
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
        public async Task<ActionResult<PaginatedDataDTO<UserInfoDTO>>> GetAllUsers(TableStateData<UserInfoSortProperty> tableStateData)
        {
            var users = await _userService.GetUsersAsync(tableStateData);
            return _mapper.Map<PaginatedDataDTO<UserInfoDTO>>(users);
        }

        [HttpGet("getbyid")]
        public async Task<ActionResult<UserInfoDTO>> GetUserById([FromQuery] string userId)
        {
            var user = await _userService.GetUserById(userId);
            return _mapper.Map<UserInfoDTO>(user);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<UserInfoDTO>> Edit([FromBody] UserInfoDTO userToEdit)
        {
            if(userToEdit.Email != HttpContext.User.Identity.Name)
            {
                var errors = new Dictionary<string, string>();
                errors.Add("User update failed", "You can't change this user information.");
                return Unauthorized(errors);
            }

            var result = await _userService.EditUser(userToEdit);

            if (result)
                return Ok();
            else
                return NotFound();
        }
    }
}