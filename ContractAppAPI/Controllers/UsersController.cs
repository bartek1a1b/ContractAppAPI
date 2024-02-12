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
    }
}
