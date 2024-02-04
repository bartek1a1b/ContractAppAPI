using ContractAppAPI.Models;

namespace ContractAppAPI.Interfaces
{
    public interface IContractTypeTwoRepository
    {
        ICollection<ContractTypeTwo> GetContractTypeTwos();
        ContractTypeTwo GetContractTypeTwo(int id);
        ICollection<Contract> GetContractByTypeTwo(int contractTypeTwoId);
        bool ContractTypeTwoExists(int id);
        bool CreateContractTypeTwo(ContractTypeTwo contractTypeTwo);
        bool UpdateContractTypeTwo(ContractTypeTwo contractTypeTwo);
        bool DeleteContractTypeTwo(ContractTypeTwo contractTypeTwo);
        bool Save();
    }
}
