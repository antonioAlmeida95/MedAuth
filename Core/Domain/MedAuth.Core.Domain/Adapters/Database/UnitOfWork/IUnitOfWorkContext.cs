using MedAuth.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MedAuth.Core.Domain.Adapters.Database.UnitOfWork;

public interface IUnitOfWorkContext
{
    DbSet<Pessoa> Pessoa { get; }
    DbSet<Perfil> Perfil { get; }
    DbSet<PerfilPermissao> PerfilPermissao { get; }
    DbSet<Permissao> Permissao { get; }
    DbSet<Usuario> Usuario { get; }

    DatabaseFacade GetDatabase();
    
    /// <summary>
    ///     Método para Obtenção da Query setando a tabela do contexto
    /// </summary>
    /// <param name="track">Trackeamento da entidade</param>
    /// <typeparam name="T">Classe Base</typeparam>
    /// <returns>Query</returns>
    IQueryable<T> GetQuery<T>(bool track) where T: class;
    
    /// <summary>
    ///     Método para Obtenção da Query setando a tabela do contexto
    /// </summary>
    /// <typeparam name="T">Classe Base</typeparam>
    /// <returns>Query</returns>
    IQueryable<T> GetQuery<T>() where T: class;
}