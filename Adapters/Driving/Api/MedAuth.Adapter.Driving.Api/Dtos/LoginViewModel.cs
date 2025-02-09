using System.ComponentModel.DataAnnotations;

namespace MedAuth.Adapter.Driving.Api.Dtos;

public class LoginViewModel
{
    public string? Email { get; set; }
    public string? Crm { get; set; }
    public string? Documento { get; set; }
    
    [Required]
    public string Password { get; set; }
}