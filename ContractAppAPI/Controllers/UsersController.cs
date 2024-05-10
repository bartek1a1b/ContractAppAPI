using AutoMapper;
using ContractAppAPI.Data;
using ContractAppAPI.Dto;
using ContractAppAPI.Interfaces;
using ContractAppAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContractAppAPI.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUserDto>>> GetUsers()
        {
            var users = await _userRepository.GetAppUsersAsync();


            return Ok(users);
        }

        [HttpGet("{email}")]
        public async Task<ActionResult<AppUserDto>> GetUser(string email)
        {
            return await _userRepository.GetAppUserAsync(email);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete-user/{id}")]
        public async Task<ActionResult> DeleteUserAsync(int id)
        {
            var userToDelete = await _userRepository.UserExists(id);

            if (!userToDelete)
            {
                return NotFound();
            }

            await _userRepository.DeleteUserAsync(id);

            return NoContent();
        }
    }
}
