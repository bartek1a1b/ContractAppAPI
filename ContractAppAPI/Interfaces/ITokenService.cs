using ContractAppAPI.Models;

namespace ContractAppAPI.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}