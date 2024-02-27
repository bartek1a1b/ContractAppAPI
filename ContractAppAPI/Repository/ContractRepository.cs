using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContractAppAPI.Data;
using ContractAppAPI.Dto;
using ContractAppAPI.Helper;
using ContractAppAPI.Interfaces;
using ContractAppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContractAppAPI.Repository
{
    public class ContractRepository : IContractRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ContractRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<PagedList<ContractDto>> GetContractsDtosAsync(UserParams userParams, string searchPhrase)
        {
            var query = _context.Contracts
                
                .Where(c => c.ContractNumber.ToString().Contains(searchPhrase) 
                || c.Name.ToLower().Contains(searchPhrase.ToLower())
                || c.ContractTypeOne.Name.ToLower().Contains(searchPhrase.ToLower()) 
                || c.ContractTypeTwo.Name.ToLower().Contains(searchPhrase.ToLower())
                || c.DateOfConclusion.ToString().Contains(searchPhrase) 
                || c.Description.Contains(searchPhrase.ToLower())
                || c.Value.ToString().Contains(searchPhrase) 
                || c.Contractor.ToLower().Contains(searchPhrase.ToLower())
                || c.Signatory.ToLower().Contains(searchPhrase.ToLower()))
                .ProjectTo<ContractDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            return await PagedList<ContractDto>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<PagedList<Contract>> GetContractsAsync(UserParams userParams)
        {
            var query = _context.Contracts
                .OrderBy(c => c.Id)
                .Include(cto => cto.ContractTypeOne)
                .Include(ctt => ctt.ContractTypeTwo)
                .AsNoTracking();

            return await PagedList<Contract>.CreateAsync(query, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<ICollection<Contract>> GetContractsAsync()
        {
            return await _context.Contracts
                .OrderBy(c => c.Id)
                .Include(cto => cto.ContractTypeOne)
                .Include(ctt => ctt.ContractTypeTwo)
                .ToListAsync();
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
