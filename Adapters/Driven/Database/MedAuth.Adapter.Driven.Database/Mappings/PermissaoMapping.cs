using MedAuth.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedAuth.Adapter.Driven.Database.Mappings;

public class PermissaoMapping : IEntityTypeConfiguration<Permissao>
{
    public void Configure(EntityTypeBuilder<Permissao> builder)
    {
        builder.Property(x => x.Id)
            .HasColumnName("Per_PermissaoId")
            .IsRequired();
        
        builder.Property(x => x.Nome)
            .HasColumnName("Per_Nome")
            .HasMaxLength(50)
            .IsRequired();
        
        builder.Property(x => x.Nome)
            .HasColumnName("Per_Valor")
            .IsRequired();
        
        builder.Ignore(x => x.ValidationResult);
        builder.Ignore(x => x.ClassLevelCascadeMode);
        builder.Ignore(x => x.RuleLevelCascadeMode);
        builder.Ignore(x => x.CascadeMode);
        
        builder.ToTable("Per_Permissao", "MedHealth");
    }
}