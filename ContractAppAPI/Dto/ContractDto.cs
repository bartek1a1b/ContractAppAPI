using ContractAppAPI.Models;

namespace ContractAppAPI.Dto
{
    public class ContractDto
    {
        public int Id { get; set; }
        public int ContractNumber { get; set; }
        public string Name { get; set; }
        public string TypeNameOne { get; set; }
        public string TypeNameTwo { get; set; }
        public DateTime DateOfConclusion { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string Contractor { get; set; }
        public string Signatory { get; set; }
        public string Pdf { get; set; }
        public ContractTypeOneDto ContractTypeOne { get; set; }
        public ContractTypeTwoDto ContractTypeTwo { get; set; }
    }
}
