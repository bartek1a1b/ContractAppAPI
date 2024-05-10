using System.ComponentModel.DataAnnotations.Schema;

namespace ContractAppAPI.Models
{
    public class ContractPdf
    {
        public int Id { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }
        public string FilePath { get; set; }
        public int ContractId { get; set; }
        public Contract Contract { get; set; }
    }
}