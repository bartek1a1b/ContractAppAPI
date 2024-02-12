using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContractAppAPI.Data;
using ContractAppAPI.Dto;
using ContractAppAPI.Interfaces;
using ContractAppAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ContractAppAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AppUserDto> GetAppUserAsync(string email)
        {
            return await _context.Users
                .Where(u => u.Email == email)
                .ProjectTo<AppUserDto>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<AppUserDto>> GetAppUsersAsync()
        {
            return await _context.Users
                .ProjectTo<AppUserDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<AppUser> GetUserByEmailAsync(string email)
        {
            return await _context.Users
                .Include(r => r.Role)
                .SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<AppUser> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<AppUser>> GetUsersAsync()
        {
            return await _context.Users
                .Include(r => r.Role)
                .ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}