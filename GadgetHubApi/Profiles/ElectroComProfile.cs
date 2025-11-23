using AutoMapper;
using GadgetHub.Models;
using GadgetHub.DTOs;

namespace GadgetHub.Profiles
{
    public class ElectroComProfile : Profile
    {
        public ElectroComProfile()
        {
            CreateMap<ProductDistributorMap, ProductDistributorMapDto>();
            CreateMap<ProductDistributorMapWriteDto, ProductDistributorMap>();
        }
    }
}


