using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace MedAuth.Core.Domain.Entities;

public class Permissao: EntidadeBase<Permissao>
{
    [MaxLength(50)] 
    public string Nome { get; }
    public int Valor { get; }
    
    public Permissao() { }

    public Permissao(string nome, int valor)
    {
        Nome = nome;
        Valor = valor;
    }

    public ICollection<PerfilPermissao> Perfis { get; set; } = [];

    public override bool ValidarEntidade()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("O Nome é um campo obrigatório");

        RuleFor(x => x.Valor)
            .GreaterThan(0)
            .WithMessage("Valor Invalido para o campo");
        
        ValidationResult = Validate(this);
        return ValidationResult.IsValid;
    }
}