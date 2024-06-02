using ContractAppAPI.Dto;
using ContractAppAPI.Models;

namespace ContractAppAPI.Interfaces
{
    public interface IContractTypeTwoRepository
    {
        ICollection<ContractTypeTwo> GetContractTypeTwos();
        ContractTypeTwo GetContractTypeTwo(int id);
        ICollection<ContractDto> GetContractByTypeTwo(int contractTypeTwoId);
        ICollection<ContractTypeTwo> GetContractTypeTwoByTypeOne(int contractTypeOneId);
        bool ContractTypeTwoExists(int id);
        bool CreateContractTypeTwo(int contractTypeOneId, ContractTypeTwo contractTypeTwo);
        bool UpdateContractTypeTwo(ContractTypeTwo contractTypeTwo);
        bool DeleteContractTypeTwo(ContractTypeTwo contractTypeTwo);
        bool Save();
    }
}
