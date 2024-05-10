using AutoMapper;
using ContractAppAPI.Data;
using ContractAppAPI.Interfaces;
using ContractAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractAppAPI.Repository
{
    public class AnnexToTheContractRepository : IAnnexToTheContractRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public AnnexToTheContractRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool AnnexToTheContractExists(int id)
        {
            return _context.AnnexToTheContracts.Any(a => a.Id == id);
        }

        public ICollection<AnnexToTheContract> GetAnnexToTheContracts()
        {
            return _context.AnnexToTheContracts
                .OrderBy(a => a.Id)
                .Include(c => c.Contract)
                .ToList();
        }

        public bool CreateAnnexToTheContract(int contractId, AnnexToTheContract annexToTheContract)
        {
            _context.AnnexToTheContracts.Add(annexToTheContract);
            return Save();
        }

        public bool DeleteAnnexToTheContract(AnnexToTheContract annexToTheContract)
        {
            _context.Remove(annexToTheContract);
            return Save();
        }

        public AnnexToTheContract GetAnnexToTheContract(int id)
        {
            return _context.AnnexToTheContracts.Where(a => a.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateAnnexToTheContract(AnnexToTheContract annexToTheContract)
        {
            _context.Update(annexToTheContract);
            return Save();
        }
    }
}