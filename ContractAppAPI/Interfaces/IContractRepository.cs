using ContractAppAPI.Dto;
using ContractAppAPI.Helper;
using ContractAppAPI.Models;

namespace ContractAppAPI.Interfaces
{
    public interface IContractRepository
    {
        Task<PagedList<ContractDto>> GetContractsDtosAsync(UserParams userParams, string searchPhrase);
        Task<ICollection<Contract>> GetContractsAsync();
        Task<PagedList<Contract>> GetContractsAsync(UserParams userParams);
        ICollection<AnnexToTheContract> GetAnnexByContract(int contractId);
        Contract GetContract(int id);
        Contract GetContract(string name);
        bool ContractExists(int conId);
        bool CreateContract(int contractTypeOneId, int contractTypeTwoId, Contract contract);
        bool UpdateContract(int contractTypeOneId, int contractTypeTwoId, Contract contract);
        bool DeleteContract(Contract contract);
        bool Save();
    }
}
