using System.ComponentModel.DataAnnotations;

namespace ContractAppAPI.Models
{
    public class AnnexToTheContract
    {
        public int Id { get; set; }
        public int AnnexNumber { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public DateTime DateOfConclusion { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        [MaxLength(50)]
        public string Contractor { get; set; }
        [MaxLength(50)]
        public string Signatory { get; set; }
        public Boolean HasPdf { get; set; } = false;
        public int ContractId { get; set; }
        public Contract Contract { get; set; }
        public ICollection<Pdf> Pdfs { get; set; }
    }
}