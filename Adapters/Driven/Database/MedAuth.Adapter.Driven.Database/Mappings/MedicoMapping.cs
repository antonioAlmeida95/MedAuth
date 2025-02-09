using MedAuth.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedAuth.Adapter.Driven.Database.Mappings;

public class MedicoMapping : IEntityTypeConfiguration<Medico>
{
    public void Configure(EntityTypeBuilder<Medico> builder)
    {
        builder.Property(x => x.Id)
            .HasColumnName("Mdc_MedicoId")
            .IsRequired();
        
        builder.Property(x => x.Crm)
            .HasColumnName("Mdc_Crm")
            .HasMaxLength(10)
            .IsRequired();
        
        builder.Property(x => x.Telefone)
            .HasColumnName("Mdc_Telefone")
            .HasMaxLength(15)
            .IsRequired();
        
        builder.Property(x => x.Especialidade)
            .HasColumnName("Mdc_Especialidade")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Status)
            .HasColumnName("Mdc_Status")
            .HasDefaultValue(false);

        builder.Property(x => x.PessoaId)
            .HasColumnName("Pes_PessoaId")
            .IsRequired();
        
        builder.HasOne(s => s.Pessoa)
            .WithOne(s => s.Medico)
            .HasForeignKey<Medico>(s => s.PessoaId)
            .IsRequired();
        
        builder.Ignore(x => x.ValidationResult);
        builder.Ignore(x => x.ClassLevelCascadeMode);
        builder.Ignore(x => x.RuleLevelCascadeMode);
        builder.Ignore(x => x.CascadeMode);
        
        builder.ToTable("Mdc_Medico", "MedHealth");
    }
}