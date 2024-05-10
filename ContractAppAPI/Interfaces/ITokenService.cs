using ContractAppAPI.Models;

namespace ContractAppAPI.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}