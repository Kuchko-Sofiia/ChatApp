﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ChatApp.DTO.Authentication;
using ChatApp.BLL.Services.Interfaces;
using ChatApp.DAL.Entities;
using System.Text;

namespace ChatApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly IMapper _mapper;

        public AccountController(IJwtTokenService jwtTokenService, IMapper mapper, UserManager<User> userManager)
        {
            _jwtTokenService = jwtTokenService;
            _mapper = mapper;
            _userManager = userManager;
        }

        [AllowAnonymous]
        [HttpGet("test22")]
        public async Task<IActionResult> Test()
        {
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                var token = _jwtTokenService.CreateToken(user);

                user.RefreshToken = _jwtTokenService.CreateRefreshToken();
                user.RefreshTokenExpiryTime = _jwtTokenService.GetRefreshTokenExpiryTime();

                return Ok(new AuthResponseDTO
                {
                    Username = user.UserName!,
                    Email = user.Email!,
                    Token = token,
                    RefreshToken = user.RefreshToken,
                    RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
                });
            }

            return Unauthorized("The username or password is incorrect");
        }

        [AllowAnonymous]
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInDTO signInDto)
        {
            var user = _mapper.Map<User>(signInDto);

            var result = await _userManager.CreateAsync(user, signInDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            var token = _jwtTokenService.CreateToken(user);

            user.RefreshToken = _jwtTokenService.CreateRefreshToken();
            user.RefreshTokenExpiryTime = _jwtTokenService.GetRefreshTokenExpiryTime();

            return Ok(new AuthResponseDTO
            {
                Username = user.UserName!,
                Email = user.Email!,
                Token = token,
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
            });
        }

        [Authorize]
        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDTO changePasswordDto)
        {
            var user = await _userManager.FindByEmailAsync(changePasswordDto.Email);

            if (user == null)
            {
                return NotFound("User is not found");
            }

            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);

            if (result.Succeeded)
            {
                var token = _jwtTokenService.CreateToken(user);

                return Ok(new { token });
            }

            return BadRequest(result.Errors);
        }

        [Authorize]
        [HttpPost("refresh-token")]
        public async Task<ActionResult<AuthResponseDTO>> RefreshToken(TokenDTO tokenModel)
        {
            var principal = _jwtTokenService.GetPrincipalFromExpiredToken(tokenModel.AccessToken);

            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var user = await _userManager.FindByNameAsync(principal.Identity!.Name);


            if (user == null || user.RefreshToken != tokenModel.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            user.RefreshToken = _jwtTokenService.CreateRefreshToken();
            user.RefreshTokenExpiryTime = _jwtTokenService.GetRefreshTokenExpiryTime();

            await _userManager.UpdateAsync(user);

            return new AuthResponseDTO()
            {
                Token = _jwtTokenService.CreateToken(user),
                RefreshToken = user.RefreshToken,
                RefreshTokenExpiryTime = user.RefreshTokenExpiryTime
            };
        }

        [Authorize]
        [HttpPost("revoke/{username}")]
        public async Task<IActionResult> Revoke (string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null) return BadRequest("Invalid user name");

            user.RefreshToken = null;
            await _userManager.UpdateAsync(user);

            return Ok();
        }

        [Authorize]
        [HttpPost("revoke-all")]
        public async Task<IActionResult> RevokeAll()
        {
            var users = _userManager.Users.ToList();
            foreach (var user in users)
            {
                user.RefreshToken = null;
                await _userManager.UpdateAsync(user);
            }

            return Ok();
        }

        [Authorize]
        [HttpGet("test")]
        public IActionResult TestAction()
        {
            return Ok(new { responseMessage = "You're authorized" });
        }
    }
}
