using AutoMapper;
using ContractAppAPI.Dto;
using ContractAppAPI.Models;

namespace ContractAppAPI.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contract, ContractDto>();
            CreateMap<ContractDto, Contract>();
            CreateMap<ContractTypeOne, ContractTypeOneDto>();
            CreateMap<ContractTypeOneDto, ContractTypeOne>();
            CreateMap<ContractTypeTwo, ContractTypeTwoDto>();
            CreateMap<ContractTypeTwoDto, ContractTypeTwo>();
        }
    }
}
