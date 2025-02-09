using MedAuth.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MedAuth.Adapter.Driven.Database.Mappings;

public class PessoaMapping : IEntityTypeConfiguration<Pessoa>
{
    public void Configure(EntityTypeBuilder<Pessoa> builder)
    {
        builder.Property(x => x.Id)
            .HasColumnName("Pes_PessoaId")
            .IsRequired();
        
        builder.Property(x => x.Nome)
            .HasColumnName("Pes_Nome")
            .HasMaxLength(100)
            .IsRequired();
        
        builder.Property(x => x.Documento)
            .HasColumnName("Pes_Documento")
            .HasMaxLength(14)
            .IsRequired();
        
        builder.Property(x => x.DataNascimento)
            .HasColumnName("Pes_DataNascimento");
        
        builder.Property(x => x.Genero)
            .HasColumnName("Pes_Genero")
            .IsRequired();

        builder.Property(x => x.PerfilId)
            .HasColumnName("Prf_PerfilId");

        builder.HasOne(s => s.Perfil)
            .WithMany(s => s.Pessoas)
            .HasForeignKey(s => s.PerfilId)
            .IsRequired();

        builder.Property(x => x.UsuarioId)
            .HasColumnName("Usu_UsuarioId");

        builder.HasOne(s => s.Usuario)
            .WithOne(s => s.Pessoa)
            .HasForeignKey<Pessoa>(s => s.UsuarioId)
            .IsRequired();
        
        builder.Ignore(x => x.ValidationResult);
        builder.Ignore(x => x.ClassLevelCascadeMode);
        builder.Ignore(x => x.RuleLevelCascadeMode);
        builder.Ignore(x => x.CascadeMode);
        
        builder.ToTable("Pes_Pessoa", "MedHealth");
    }
}