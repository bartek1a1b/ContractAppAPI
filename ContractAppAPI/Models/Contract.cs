using System.ComponentModel.DataAnnotations;

namespace ContractAppAPI.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public int ContractNumber { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public DateTime DateOfConclusion { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
        public decimal Value { get; set; }
        [MaxLength(50)]
        public string Contractor { get; set; }
        [MaxLength(50)]
        public string Signatory { get; set; }
        public Boolean HasPdf { get; set; }
        public int ContractTypeOneId { get; set; }
        public int ContractTypeTwoId { get; set; }
        public ContractTypeOne ContractTypeOne { get; set; }
        public ContractTypeTwo ContractTypeTwo { get; set; }
        public ICollection<ContractPdf> ContractPdfs { get; set; }
        public ICollection<AnnexToTheContract> AnnexToTheContracts { get; set; }
    }
}
