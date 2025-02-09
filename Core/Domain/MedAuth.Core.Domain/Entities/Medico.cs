using System.ComponentModel.DataAnnotations;
using FluentValidation;

namespace MedAuth.Core.Domain.Entities;

public class Medico : EntidadeBase<Medico>
{
    [Required]
    [MaxLength(10)]
    public string Crm { get; }
    
    [Required]
    [MaxLength(15)]
    public string Telefone { get; }

    [Required]
    [MaxLength(50)]
    public string Especialidade { get; }

    public bool Status { get; }
    
    public Guid PessoaId { get; set; }
    public virtual Pessoa Pessoa { get; set; }
    
    public Medico(string crm, string telefone, string especialidade) 
        : this(crm, telefone, especialidade, null){ }

    public Medico(string crm, string telefone, string especialidade, bool? status = null, Guid? id = null)
    {
        Id = id ?? Guid.NewGuid();
        Crm = crm;
        Telefone = telefone;
        Especialidade = especialidade;
        Status = status ?? false;
    }
    
    public Medico() { }
    
    public override bool ValidarEntidade()
    {
        RuleFor(x => x.Crm)
            .NotEmpty()
            .WithMessage("O Crm é um campo obrigatório");
        
        RuleFor(x => x.Telefone)
            .NotEmpty()
            .WithMessage("O Telefone é um campo obrigatório");
        
        ValidationResult = Validate(this);
        return ValidationResult.IsValid;
    }
}