using MedAuth.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedAuth.Adapter.Driven.Database.Mappings;

public class PerfilMapping : IEntityTypeConfiguration<Perfil>
{
    public void Configure(EntityTypeBuilder<Perfil> builder)
    {
        builder.Property(x => x.Id)
            .HasColumnName("Prf_PerfilId")
            .IsRequired();

        builder.Property(x => x.Nome)
            .HasColumnName("Prf_Nome")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(x => x.Status)
            .HasColumnName("Prf_Status")
            .HasDefaultValue(false)
            .IsRequired();
        
        builder.Ignore(x => x.ValidationResult);
        builder.Ignore(x => x.ClassLevelCascadeMode);
        builder.Ignore(x => x.RuleLevelCascadeMode);
        builder.Ignore(x => x.CascadeMode);
        
        builder.ToTable("Prf_Perfil", "MedHealth");
    }
}