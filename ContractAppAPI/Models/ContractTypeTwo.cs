using System.ComponentModel.DataAnnotations;

namespace ContractAppAPI.Models
{
    public class ContractTypeTwo
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public int ContractTypeOneId { get; set; }
        public ContractTypeOne ContractTypeOne { get; set; }
        public ICollection<Contract> Contracts { get; set; }
    }
}
