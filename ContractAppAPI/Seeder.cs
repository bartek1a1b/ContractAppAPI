using ContractAppAPI.Data;
using ContractAppAPI.Models;

namespace ContractAppAPI
{
    public class Seeder
    {
        private readonly DataContext _dataContext;
        public Seeder(DataContext context)
        {
            _dataContext = context;
        }
        public void SeedDataContext()
        {
            if (_dataContext.Database.CanConnect())
            {
                if (!_dataContext.Contracts.Any())
                {
                    var contracts = GetContracts();
                    _dataContext.Contracts.AddRange(contracts);
                    _dataContext.SaveChanges();
                }
            }
        }
            private IEnumerable<Contract> GetContracts()
            {
                var contracts = new List<Contract>()
                {
                    new Contract()
                    {
                        ContractNumber = 1,
                        Name = "Umowa testowa 1",
                        DateOfConclusion = new DateTime(2020,1,1),
                        Description = "Opis umowy",
                        Value = 1200,
                        Contractor = "Jan Nowak",
                        Signatory = "Jan Kowalski",
                        HasPdf = false,
                        //Pdf = "Link",
                        ContractTypeOne = new ContractTypeOne()
                        {
                            Name = "Umowa unijna"
                        },
                        ContractTypeTwo = new ContractTypeTwo()
                        {
                            Name = "Umowa katering"
                        }
                    },
                    new Contract()
                    {
                        ContractNumber = 2,
                        Name = "Umowa testowa 2",
                        DateOfConclusion = new DateTime(2021,11,12),
                        Description = "Opis umowy",
                        Value = 1300,
                        Contractor = "Jan Nowak",
                        Signatory = "Jan Kowalski",
                        HasPdf = false,
                        //Pdf = "Link",
                        ContractTypeOne = new ContractTypeOne()
                        {
                            Name = "Umowa projekt x power"
                        },
                        ContractTypeTwo = new ContractTypeTwo()
                        {
                            Name = "Umowa kwestury"
                        }
                    }
                };
                return contracts;
            }
    }
}
