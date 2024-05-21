using ContractAppAPI.Data;
using ContractAppAPI.Interfaces;
using ContractAppAPI.Models;
using Microsoft.EntityFrameworkCore;

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
            return _context.ContractTypeOnes
                .Include(cto => cto.ContractTypeTwos)
                .FirstOrDefault(t => t.Id == id);
        }

        public ICollection<ContractTypeOne> GetContractTypeOnes()
        {
            return _context.ContractTypeOnes
                .Include(cto => cto.ContractTypeTwos)
                .ToList();
        }

        public ICollection<ContractTypeTwo> GetTypeTwoByTypeOne(int contractTypeOneId)
        {
            return _context.ContractTypeTwos.Where(ctt => ctt.ContractTypeOneId == contractTypeOneId).ToList();
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
