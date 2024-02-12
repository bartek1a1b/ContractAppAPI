using ContractAppAPI.Dto;
using ContractAppAPI.Models;

namespace ContractAppAPI.Interfaces
{
    public interface IUserRepository
    {
        void Update(AppUser user);
        Task<bool> SaveAllAsync();
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser> GetUserByEmailAsync(string email);
        Task<IEnumerable<AppUserDto>> GetAppUsersAsync();
        Task<AppUserDto> GetAppUserAsync(string email);
    }
}