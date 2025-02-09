using MedAuth.Adapter.Driven.Database.Repository;
using MedAuth.Adapter.Driven.Database.UnitOfWork;
using MedAuth.Core.Domain.Adapters.Database.Repository;
using MedAuth.Core.Domain.Adapters.Database.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UnitOfWorkInstance = MedAuth.Adapter.Driven.Database.UnitOfWork.UnitOfWork;

namespace MedAuth.Adapter.Driven.Database;

public static class MedAuthDatabaseDependency
{
    public static void AddMedAuthDatabaseModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWorkInstance>();
        services.AddScoped(_ =>
        {
            var connectionString = GetConnectionString(configuration);
            var context = new UnitOfWorkContext(connectionString);
            
            if (context.Database.GetPendingMigrations().Any())
                context.Database.Migrate();
            
            return context;
        });
        
        services.AddScoped<IUsuarioRepository, UsuarioRepository>();
    }
    
    private static string? GetConnectionString(IConfiguration configuration)
    {
        var connectionStringName = "DefaultConnection";
       
#if !DEBUG
        connectionStringName = "DockerConnection";
#endif

        return configuration.GetConnectionString(connectionStringName);
    }
}