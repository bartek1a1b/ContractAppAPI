using System.ComponentModel.DataAnnotations;

namespace ContractAppAPI.Models
{
    public class ContractTypeOne
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public ICollection<Contract> Contracts { get; set; }
        public ICollection<ContractTypeTwo> ContractTypeTwos { get; set; }
    }
}
