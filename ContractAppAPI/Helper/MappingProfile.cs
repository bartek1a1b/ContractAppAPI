using AutoMapper;
using ContractAppAPI.Dto;
using ContractAppAPI.Models;

namespace ContractAppAPI.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Contract, ContractDto>()
                .ForMember(dest => dest.TypeNameOne, opt => opt.MapFrom(src => src.ContractTypeOne.Name))
                .ForMember(dest => dest.TypeNameTwo, opt => opt.MapFrom(src => src.ContractTypeTwo.Name));
            CreateMap<ContractDto, Contract>();
            CreateMap<Contract, ContractAddDto>();
            CreateMap<ContractAddDto, Contract>();
            CreateMap<ContractTypeOne, ContractTypeOneDto>();
            CreateMap<ContractTypeOneDto, ContractTypeOne>();
            CreateMap<ContractTypeTwo, ContractTypeTwoDto>();
            CreateMap<ContractTypeTwoDto, ContractTypeTwo>();
            CreateMap<AppUser, AppUserDto>()
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
            CreateMap<Role, RoleDto>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<ChangePasswordDto, AppUser>();
            CreateMap<AppUser, ChangePasswordDto>();
        }
    }
}
