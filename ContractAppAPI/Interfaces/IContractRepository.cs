using ContractAppAPI.Models;

namespace ContractAppAPI.Interfaces
{
    public interface IContractRepository
    {
        ICollection<Contract> GetContracts();
        Contract GetContract(int id);
        Contract GetContract(string name);
        bool ContractExists(int conId);
        bool CreateContract(int contractTypeOneId, int contractTypeTwoId, Contract contract);
        bool UpdateContract(int contractTypeOneId, int contractTypeTwoId, Contract contract);
        bool DeleteContract(Contract contract);
        bool Save();
    }
}
