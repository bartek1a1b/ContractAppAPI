using ContractAppAPI.Models;

namespace ContractAppAPI.Dto
{
    public class ContractTypeOneDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<string> ContractTypeTwoDtos { get; set; }
    }
}
