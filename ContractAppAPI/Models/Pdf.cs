using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContractAppAPI.Models
{
    public class Pdf
    {
        public int Id { get; set; }
        [NotMapped]
        public IFormFile? File { get; set; }
        [MaxLength(50)]
        public string FilePath { get; set; }
        public int AnnexToTheContractId { get; set; }
        public AnnexToTheContract AnnexToTheContract { get; set; }
    }
}