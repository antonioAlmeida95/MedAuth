using MedAuth.Core.Application.Models;
using MedAuth.Core.Application.Services;
using MedAuth.Core.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace MedAuth.Core.Application;

public static class MedAuthApplicationDependency
{
    public static void AddMedAuthApplicationModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CredentialSettings>(opt => configuration.GetSection("Credentials:Secret").Bind(opt));
        services.AddScoped<IAuthService, AuthService>();
    }
}