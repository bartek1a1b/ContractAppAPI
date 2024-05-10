using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using ContractAppAPI.Data;
using ContractAppAPI.Dto;
using ContractAppAPI.Interfaces;
using ContractAppAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContractAppAPI.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.UserName)) return BadRequest("Email jest zajęty");

            var user = _mapper.Map<AppUser>(registerDto);

            user.Email = registerDto.Email;
            user.UserName = registerDto.UserName;
            user.FirstName = registerDto.FirstName;
            user.LastName = registerDto.LastName;

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            var roleResult = await _userManager.AddToRoleAsync(user, "Reader");

            if (!roleResult.Succeeded) return BadRequest(result.Errors);

            return new UserDto
            {
                Email = user.Email,
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u =>
                u.UserName == loginDto.UserName);

            if (user == null) return Unauthorized("Błędny adres email");

            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if (!result) return Unauthorized("Błędne hasło");

            return new UserDto
            {
                Email = user.Email,
                UserName = user.UserName,
                Token = await _tokenService.CreateToken(user)
            };
        }

        // [HttpPut("change-password")]
        // public async Task<ActionResult<UserDto>> ChangePassword(ChangePasswordDto changePasswordDto)
        // {
        //     var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == changePasswordDto.Email);

        //     if (user == null) 
        //     {
        //         return NotFound("Użytkownik nie istnieje");
        //     }

        //     using var newHmac = new HMACSHA512();
        //     var newHash = newHmac.ComputeHash(Encoding.UTF8.GetBytes(changePasswordDto.NewPassword));

        //     // user.PasswordHash = newHash;
        //     // user.PasswordSalt = newHmac.Key;

        //     await _context.SaveChangesAsync();

        //     return new UserDto
        //     {
        //         Email = user.Email,
        //         Token = _tokenService.CreateToken(user)
        //     };
        // }

        private async Task<bool> UserExists(string email)
        {
            return await _userManager.Users.AnyAsync(u => u.Email == email);
        }
    }
}
