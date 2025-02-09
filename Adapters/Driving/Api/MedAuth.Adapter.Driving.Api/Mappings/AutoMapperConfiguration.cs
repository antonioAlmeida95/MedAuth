using AutoMapper;

namespace MedAuth.Adapter.Driving.Api.Mappings;

public static class AutoMapperConfiguration
{
    public static MapperConfiguration RegisterMappings()
    {
        return new MapperConfiguration(ps =>
        {
            ps.AddProfile(new ViewModelToDomainMappingProfile());
        });
    }
}