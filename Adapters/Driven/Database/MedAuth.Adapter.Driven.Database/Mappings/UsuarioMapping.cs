using MedAuth.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedAuth.Adapter.Driven.Database.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.Property(x => x.Id)
            .HasColumnName("Usu_UsuarioId")
            .IsRequired();
        
        builder.Property(x => x.Email)
            .HasColumnName("Usu_Email")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Senha)
            .HasColumnName("Usu_Senha")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Status)
            .HasColumnName("Usu_Status")
            .HasDefaultValue(false);
        
        builder.Ignore(x => x.ValidationResult);
        builder.Ignore(x => x.ClassLevelCascadeMode);
        builder.Ignore(x => x.RuleLevelCascadeMode);
        builder.Ignore(x => x.CascadeMode);
        
        builder.ToTable("Usu_Usuario", "MedHealth");
    }
}