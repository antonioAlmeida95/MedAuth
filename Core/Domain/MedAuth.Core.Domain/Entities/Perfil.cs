using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace MedAuth.Core.Domain.Entities;

public class Perfil : EntidadeBase<Perfil>
{
    [MaxLength(50)]
    public string Nome { get; }

    public bool Status { get; }

    public ICollection<PerfilPermissao> Permissoes { get; set; } = [];
    public ICollection<Pessoa> Pessoas { get; set; } = [];
    
    public Perfil() { }

    public Perfil(string nome, bool status)
    {
        Nome = nome;
        Status = status;
    }

    public override bool ValidarEntidade()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("O Nome é um campo obrigatório");
        
        ValidationResult = Validate(this);
        return ValidationResult.IsValid;
    }
}