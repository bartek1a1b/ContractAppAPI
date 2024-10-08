﻿using AutoMapper;
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
            CreateMap<ContractTypeOne, ContractTypeOneDto>()
                .ForMember(dest => dest.ContractTypeTwoDtos, opt => opt.MapFrom(src => src.ContractTypeTwos.Select(ctt => ctt.Name).ToList()));
            CreateMap<ContractTypeOneDto, ContractTypeOne>();
            CreateMap<ContractTypeOne, ContractTypeOneAddDto>();
            CreateMap<ContractTypeOneAddDto, ContractTypeOne>();
            CreateMap<ContractTypeTwo, ContractTypeTwoDto>();
            CreateMap<ContractTypeTwoDto, ContractTypeTwo>();
            CreateMap<ContractTypeTwo, ContractTypeTwoAddDto>();
            CreateMap<ContractTypeTwoAddDto, ContractTypeTwo>();
            CreateMap<AnnexToTheContract, AnnexToTheContractDto>()
                .ForMember(dest => dest.ContractNumber, opt => opt.MapFrom(src => src.Contract.ContractNumber));
            CreateMap<AnnexToTheContractDto, AnnexToTheContract>();
            CreateMap<AnnexToTheContract, AnnexToTheContractAddDto>();
            CreateMap<AnnexToTheContractAddDto, AnnexToTheContract>();
            CreateMap<ContractPdf, ContractPdfDto>();
            CreateMap<ContractPdfDto, ContractPdf>();
            CreateMap<Pdf, PdfDto>();
            CreateMap<PdfDto, PdfDto>();
            CreateMap<AppUser, AppUserDto>();
            //     .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
            // CreateMap<Role, RoleDto>();
            CreateMap<RegisterDto, AppUser>();
            CreateMap<ChangePasswordDto, AppUser>();
            CreateMap<AppUser, ChangePasswordDto>();
        }
    }
}
