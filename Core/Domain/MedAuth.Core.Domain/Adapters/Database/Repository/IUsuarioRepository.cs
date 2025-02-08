using System.Linq.Expressions;
using MedAuth.Core.Domain.Entities;

namespace MedAuth.Core.Domain.Adapters.Database.Repository;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterUsuarioPorFiltroAsync(Expression<Func<Usuario, bool>> predicate);
}