using MedAuth.Core.Domain.Entities;

namespace MedAuth.Core.Domain.Services;

public interface IAuthService
{
    Task<string> GenerateToken(Login login);
}