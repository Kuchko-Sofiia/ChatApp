using AutoMapper;
using ChatApp.DTO;
using ChatApp.BLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ChatApp.DAL.Entities;

namespace ChatApp.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet("getbyid")]
        public async Task<ActionResult<UserDTO>> GetUserById([FromQuery] string userId)
        {
            var user = await _userService.GetUserById(userId);
            return _mapper.Map<UserDTO>(user);
        }

        [HttpPost("getpaginatedusers")]
        public async Task<ActionResult<PaginatedDataDTO<UserDTO>>> GetPaginatedUsers(PaginatedDataStateDTO<UserInfoSortProperty> tableStateData)
        {
            var users = await _userService.GetUsersAsync(tableStateData);
            return _mapper.Map<PaginatedDataDTO<UserDTO>>(users);
        }

        [HttpPut("edit")]
        public async Task<ActionResult<UserDTO>> Edit(UserDTO userToEdit)
        {
            if (userToEdit.Email != HttpContext.User.Identity.Name)
            {
                var errors = new Dictionary<string, string>
                {
                    { "User update failed", "You can't change this user information." }
                };
                return Unauthorized(errors);
            }

            var result = await _userService.EditUser(userToEdit);

            if (result)
                return Ok();
            else
                return NotFound();
        }

        [HttpPost("avatar/add")]
        public async Task<ActionResult<AvatarDTO>> AddUserAvatar(AvatarDTO newAvatar)
        {
            var avatar = _mapper.Map<Avatar>(newAvatar);
            await _userService.AddAvatarAsync(avatar);
            return Ok();
        }

        [HttpDelete("avatar/remove/{id:int}")]
        public async Task<ActionResult<AvatarDTO>> RemoveUserAvatar(int id)
        {
            await _userService.RemoveAvatarAsync(id);
            return Ok();
        }
    }
}