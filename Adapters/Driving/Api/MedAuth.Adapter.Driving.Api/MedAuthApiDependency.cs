using MedAuth.Adapter.Driving.Api.AppService;
using MedAuth.Adapter.Driving.Api.AppService.Interfaces;
using MedAuth.Adapter.Driving.Api.Mappings;

namespace MedAuth.Adapter.Driving.Api;

public static class MedAuthApiDependency
{
    public static void AddMedAuthApiModule(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapperConfiguration));
        services.AddScoped<IAuthAppService, AuthAppService>();
    }
}