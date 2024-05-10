using ContractAppAPI.Models;

namespace ContractAppAPI.Interfaces
{
    public interface IAnnexToTheContractRepository
    {
        ICollection<AnnexToTheContract> GetAnnexToTheContracts();
        AnnexToTheContract GetAnnexToTheContract(int id);
        bool AnnexToTheContractExists(int id);
        bool CreateAnnexToTheContract(int contractId, AnnexToTheContract annexToTheContract);
        bool UpdateAnnexToTheContract(AnnexToTheContract annexToTheContract);
        bool DeleteAnnexToTheContract(AnnexToTheContract annexToTheContract);
        bool Save();
    }
}