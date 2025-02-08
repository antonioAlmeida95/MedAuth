using FluentValidation;
using MedAuth.Core.Domain.Entities.Enums;

namespace MedAuth.Core.Domain.Entities;

public class Pessoa : EntidadeBase<Pessoa>
{
    public string Nome { get; }
    public string Documento { get; }
    public DateTimeOffset DataNascimento { get; }
    public Genero Genero { get; }

    public Guid PerfilId { get; set; }
    public virtual Perfil Perfil { get; set; }

    public Guid UsuarioId { get; set; }
    public virtual Usuario Usuario { get; set; }
    
    public virtual Medico Medico { get; set; }

    public Pessoa(string nome, string documento, DateTimeOffset dataNascimento, Genero genero, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Nome = nome;
        Documento = documento;
        DataNascimento = dataNascimento;
        Genero = genero;
    }

    public Pessoa(string nome, string documento, DateTimeOffset dataNascimento, Genero genero)
        : this(nome, documento, dataNascimento, genero, null) { }

    public Pessoa() { }

    public override bool ValidarEntidade()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("O Nome é um campo obrigatório");
        
        RuleFor(x => x.Documento)
            .NotEmpty()
            .WithMessage("O Documento é um campo obrigatório");
        
        ValidationResult = Validate(this);
        return ValidationResult.IsValid;
    }
}