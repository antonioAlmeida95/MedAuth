using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace MedAuth.Core.Domain.Entities;

public class PerfilPermissao : EntidadeBase<PerfilPermissao>
{
    [Required]
    public Guid PerfilId { get; }
    public virtual Perfil Perfil { get; set; }

    [Required]
    public Guid PermissaoId { get; }
    public virtual Permissao Permissao { get; set; }

    public PerfilPermissao(Guid perfilId, Guid permissaoId)
    {
        PerfilId = perfilId;
        PermissaoId = permissaoId;
    }
    
    public PerfilPermissao(){}

    public override bool ValidarEntidade()
    {
        RuleFor(x => x.PerfilId)
            .NotEmpty()
            .WithMessage("O Id perfil é um campo obrigatório");
        
        RuleFor(x => x.PermissaoId)
            .NotEmpty()
            .WithMessage("O Id permissao é um campo obrigatório");
        
        ValidationResult = Validate(this);
        return ValidationResult.IsValid;
    }
}