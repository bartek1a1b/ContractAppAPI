using AutoMapper;
using ContractAppAPI.Data;
using ContractAppAPI.Interfaces;
using ContractAppAPI.Models;

namespace ContractAppAPI.Repository
{
    public class ContractTypeTwoRepository : IContractTypeTwoRepository
    {
        private readonly DataContext _context;
        public ContractTypeTwoRepository(DataContext context)
        {
            _context = context;
        }
        public bool ContractTypeTwoExists(int id)
        {
            return _context.ContractTypeTwos.Any(t => t.Id == id);
        }

        public bool CreateContractTypeTwo(int contractTypeOneId, ContractTypeTwo contractTypeTwo)
        {
            _context.ContractTypeTwos.Add(contractTypeTwo);
            return Save();
        }

        public bool DeleteContractTypeTwo(ContractTypeTwo contractTypeTwo)
        {
            _context.Remove(contractTypeTwo);
            return Save();
        }

        public ICollection<Contract> GetContractByTypeTwo(int contractTypeTwoId)
        {
            return _context.Contracts.Where(c => c.ContractTypeTwo.Id == contractTypeTwoId).ToList();
        }

        public ContractTypeTwo GetContractTypeTwo(int id)
        {
            return _context.ContractTypeTwos.Where(t => t.Id == id).FirstOrDefault();
        }

        public ICollection<ContractTypeTwo> GetContractTypeTwos()
        {
            return _context.ContractTypeTwos.ToList();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateContractTypeTwo(ContractTypeTwo contractTypeTwo)
        {
            _context.Update(contractTypeTwo);
            return Save();
        }
    }
}
