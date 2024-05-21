namespace ContractAppAPI.Models
{
    public class ContractTypeOne
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Contract> Contracts { get; set; }
        public ICollection<ContractTypeTwo> ContractTypeTwos { get; set; }
    }
}
