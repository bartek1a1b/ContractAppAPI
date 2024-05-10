using ContractAppAPI.Dto;
using ContractAppAPI.Models;

namespace ContractAppAPI.Interfaces
{
    public interface IPdfRepository
    {
        Task<PdfDto> UploadPdf(IFormFile file);
        Task<Pdf> DeletePdf(int id);
    }
}