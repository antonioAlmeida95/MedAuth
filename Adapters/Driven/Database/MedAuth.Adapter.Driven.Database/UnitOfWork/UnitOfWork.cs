using MedAuth.Core.Domain.Adapters.Database.Repository;
using MedAuth.Core.Domain.Adapters.Database.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace MedAuth.Adapter.Driven.Database.UnitOfWork;

public class UnitOfWork(IServiceProvider serviceProvider) : IUnitOfWork
{
    public IUsuarioRepository Usuario => serviceProvider.GetRequiredService<IUsuarioRepository>();
}