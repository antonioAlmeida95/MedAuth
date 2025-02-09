using System.Linq.Expressions;
using MedAuth.Adapter.Driven.Database.UnitOfWork;
using MedAuth.Core.Domain.Adapters.Database.Repository;
using MedAuth.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace MedAuth.Adapter.Driven.Database.Repository;

public class UsuarioRepository : IUsuarioRepository,  IDisposable, IAsyncDisposable
{
    private readonly UnitOfWorkContext _context;
    
    public UsuarioRepository(UnitOfWorkContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> ObterUsuarioPorFiltroAsync(Expression<Func<Usuario, bool>> predicate)
        => await Query(predicate,
                include: i => i.Include(u => u.Pessoa)
                    .ThenInclude(p => p.Perfil)
                    .ThenInclude(p => p.Permissoes)
                    .ThenInclude(p => p.Permissao)
                    .Include(u => u.Pessoa)
                    .ThenInclude(p => p.Medico))
            .FirstOrDefaultAsync();
    
    private IQueryable<TX> Query<TX>(Expression<Func<TX, bool>>? expression = null, 
        Func<IQueryable<TX>, IIncludableQueryable<TX, object>>? include = null, bool track = false,
        int? skip = null, int? take = null) where TX : class
    {
        var query = _context.GetQuery<TX>(track);

        if (expression != null)
            query = query.Where(expression);
        
        if (include != null)
            query = include(query);

        if (skip.HasValue)
            query = query.Skip(skip.Value);

        if (take.HasValue)
            query = query.Take(take.Value);

        return query;
    }
    
    public void Dispose() => _context.Dispose();

    public async ValueTask DisposeAsync() => await _context.DisposeAsync();
}