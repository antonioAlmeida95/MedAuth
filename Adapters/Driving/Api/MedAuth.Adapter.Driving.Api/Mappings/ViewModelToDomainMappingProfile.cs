using AutoMapper;
using MedAuth.Adapter.Driving.Api.Dtos;
using MedAuth.Core.Domain.Entities;

namespace MedAuth.Adapter.Driving.Api.Mappings;

public class ViewModelToDomainMappingProfile : Profile
{
    public ViewModelToDomainMappingProfile()
    {
        CreateMap<LoginViewModel, Login>();
    }
}