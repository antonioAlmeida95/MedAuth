using System.Diagnostics.CodeAnalysis;
using MedAuth.Adapter.Driven.Database.Mappings;
using MedAuth.Core.Domain.Adapters.Database.UnitOfWork;
using MedAuth.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace MedAuth.Adapter.Driven.Database.UnitOfWork;

[ExcludeFromCodeCoverage]
public partial class UnitOfWorkContext : DbContext, IUnitOfWorkContext
{
    public DbSet<Pessoa> Pessoa { get; set; }
    public DbSet<Medico> Medico { get; set; }
    public DbSet<Perfil> Perfil { get; set; }
    public DbSet<PerfilPermissao> PerfilPermissao { get; set; }
    public DbSet<Permissao> Permissao { get; set; }
    public DbSet<Usuario> Usuario { get; set; }
    
    private string? _connectionString;
    
    public UnitOfWorkContext(string? connectionString = null) => _connectionString = connectionString;

    public DatabaseFacade GetDatabase() => Database;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MedicoMapping());
        modelBuilder.ApplyConfiguration(new PessoaMapping());
        modelBuilder.ApplyConfiguration(new UsuarioMapping());
        modelBuilder.ApplyConfiguration(new PerfilMapping());
        modelBuilder.ApplyConfiguration(new PermissaoMapping());
        modelBuilder.ApplyConfiguration(new PerfilPermissaoMapping());
    }
}