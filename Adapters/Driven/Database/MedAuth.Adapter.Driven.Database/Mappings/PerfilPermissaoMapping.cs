using MedAuth.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedAuth.Adapter.Driven.Database.Mappings;

public class PerfilPermissaoMapping : IEntityTypeConfiguration<PerfilPermissao>
{
    public void Configure(EntityTypeBuilder<PerfilPermissao> builder)
    {
        builder.Property(x => x.Id)
            .HasColumnName("Prp_PerfilPermissaoId")
            .IsRequired();

        builder.Property(x => x.PermissaoId)
            .HasColumnName("Per_PermissaoId");
        
        builder.HasOne(s => s.Permissao)
            .WithMany(s => s.Perfis)
            .HasForeignKey(s => s.PermissaoId);

        builder.Property(x => x.PerfilId)
            .HasColumnName("Prf_PerfilId");
        
        builder.HasOne(s => s.Perfil)
            .WithMany(s => s.Permissoes)
            .HasForeignKey(s => s.PerfilId);
        
        builder.Ignore(x => x.ValidationResult);
        builder.Ignore(x => x.ClassLevelCascadeMode);
        builder.Ignore(x => x.RuleLevelCascadeMode);
        builder.Ignore(x => x.CascadeMode);
        
        builder.ToTable("Prp_PerfilPermissao", "MedHealth");
    }
}