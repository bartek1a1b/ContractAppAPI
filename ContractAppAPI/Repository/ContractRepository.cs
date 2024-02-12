using ContractAppAPI.Data;
using ContractAppAPI.Interfaces;
using ContractAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractAppAPI.Repository
{
    public class ContractRepository : IContractRepository
    {
        private readonly DataContext _context;

        public ContractRepository(DataContext context)
        {
            _context = context;
        }

        public bool ContractExists(int conId)
        {
            return _context.Contracts.Any(c => c.Id == conId);
        }

        public bool CreateContract(int contractTypeOneId, int contractTypeTwoId, Contract contract)
        {
            _context.Contracts.Add(contract);
            return Save();
        }

        public bool DeleteContract(Contract contract)
        {
            _context.Remove(contract);
            return Save();
        }

        public Contract GetContract(int id)
        {
            return _context.Contracts
                .Where(c => c.Id == id)
                .Include(cto => cto.ContractTypeOne)
                .Include(ctt => ctt.ContractTypeTwo)
                .FirstOrDefault();
        }

        public Contract GetContract(string name)
        {
            return _context.Contracts
                .Where(c => c.Name == name)
                .Include(cto => cto.ContractTypeOne)
                .Include(ctt => ctt.ContractTypeTwo)
                .FirstOrDefault();
        }

        public ICollection<Contract> GetContracts()
        {
            return _context.Contracts
                .OrderBy(c => c.Id)
                .Include(cto => cto.ContractTypeOne)
                .Include(ctt => ctt.ContractTypeTwo)
                .ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateContract(int contractTypeOneId, int contractTypeTwoId, Contract contract)
        {
            _context.Update(contract);
            return Save();
        }
    }
}
