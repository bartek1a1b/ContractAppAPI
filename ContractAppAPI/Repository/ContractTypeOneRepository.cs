using ContractAppAPI.Data;
using ContractAppAPI.Interfaces;
using ContractAppAPI.Models;

namespace ContractAppAPI.Repository
{
    public class ContractTypeOneRepository : IContractTypeOneRepository
    {
        private readonly DataContext _context;
        public ContractTypeOneRepository(DataContext context)
        {
            _context = context;
        }

        public bool ContractTypeOneExists(int id)
        {
            return _context.ContractTypeOnes.Any(t => t.Id == id);
        }

        public bool CreateContractTypeOne(ContractTypeOne contractTypeOne)
        {
            _context.ContractTypeOnes.Add(contractTypeOne);
            return Save();
        }

        public bool DeleteContractTypeOne(ContractTypeOne contractTypeOne)
        {
            _context.Remove(contractTypeOne);
            return Save();
        }

        public ICollection<Contract> GetContractByTypeOne(int contractTypeOneId)
        {
            return _context.Contracts.Where(c => c.ContractTypeTwo.Id == contractTypeOneId).ToList();
        }

        public ContractTypeOne GetContractTypeOne(int id)
        {
            return _context.ContractTypeOnes.Where(t => t.Id == id).FirstOrDefault();
        }

        public ICollection<ContractTypeOne> GetContractTypeOnes()
        {
            return _context.ContractTypeOnes.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateContractTypeOne(ContractTypeOne contractTypeOne)
        {
            _context.Update(contractTypeOne);
            return Save();
        }
    }
}
