using ContractAppAPI.Dto;
using ContractAppAPI.Models;

namespace ContractAppAPI.Interfaces
{
    public interface IContractTypeOneRepository
    {
        ICollection<ContractTypeOne> GetContractTypeOnes();
        ContractTypeOne GetContractTypeOne(int id);
        ICollection<ContractDto> GetContractByTypeOne(int contractTypeOneId);
        ICollection<ContractTypeTwo> GetTypeTwoByTypeOne(int contractTypeOneId);
        bool ContractTypeOneExists(int id);
        bool CreateContractTypeOne(ContractTypeOne contractTypeOne);
        bool UpdateContractTypeOne(ContractTypeOne contractTypeOne);
        bool DeleteContractTypeOne(ContractTypeOne contractTypeOne);
        bool Save();
    }
}
