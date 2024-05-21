namespace ContractAppAPI.Models
{
    public class ContractTypeTwo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ContractTypeOneId { get; set; }
        public ContractTypeOne ContractTypeOne { get; set; }
        public ICollection<Contract> Contracts { get; set; }
    }
}
