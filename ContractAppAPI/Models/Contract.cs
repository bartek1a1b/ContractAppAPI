namespace ContractAppAPI.Models
{
    public class Contract
    {
        public int Id { get; set; }
        public int ContractNumber { get; set; }
        public string Name { get; set; }
        public DateTime DateOfConclusion { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string Contractor { get; set; }
        public string Signatory { get; set; }
        public Boolean HasPdf { get; set; } = false;
        public ContractTypeOne ContractTypeOne { get; set; }
        public ContractTypeTwo ContractTypeTwo { get; set; }
        public ICollection<ContractPdf> ContractPdfs { get; set; }
        public ICollection<AnnexToTheContract> AnnexToTheContracts { get; set; }
    }
}
