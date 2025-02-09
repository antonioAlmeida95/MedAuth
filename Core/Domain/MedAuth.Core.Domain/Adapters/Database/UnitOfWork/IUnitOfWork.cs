using MedAuth.Core.Domain.Adapters.Database.Repository;

namespace MedAuth.Core.Domain.Adapters.Database.UnitOfWork;

public interface IUnitOfWork
{
    IUsuarioRepository Usuario { get; }
}