using ContractAppAPI.Dto;
using ContractAppAPI.Models;

namespace ContractAppAPI.Interfaces
{
    public interface IContractPdfRepository
    {
        Task<ContractPdfDto> UploadPdf(IFormFile file);
        Task<ContractPdf> DeleteContractPdf(int id);
    }
}