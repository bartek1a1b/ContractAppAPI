using ContractAppAPI.Models;

namespace ContractAppAPI.Dto
{
    public class AnnexToTheContractDto
    {
        public int Id { get; set; }
        public int AnnexNumber { get; set; }
        public int ContractNumber { get; set; }
        public string Name { get; set; }
        public DateTime DateOfConclusion { get; set; }
        public string Description { get; set; }
        public string Contractor { get; set; }
        public string Signatory { get; set; }
        public Boolean HasPdf { get; set; }
        public Contract Contract { get; set; }
    }
}