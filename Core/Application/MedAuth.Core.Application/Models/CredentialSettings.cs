namespace MedAuth.Core.Application.Models;

public class CredentialSettings
{
    public string SecretKey { get; set; }
    public string Salt { get; set; }
}