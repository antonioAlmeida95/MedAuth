using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MedAuth.Core.Application.Models;
using MedAuth.Core.Domain.Adapters.Database.UnitOfWork;
using MedAuth.Core.Domain.Entities;
using MedAuth.Core.Domain.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MedAuth.Core.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly CredentialSettings _credentialSettings;

    public AuthService(IOptions<CredentialSettings> credentialSettings,
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _credentialSettings = credentialSettings.Value;
    }
    
    public async Task<string> GenerateToken(Login login)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_credentialSettings.SecretKey);

        var usuario = await ObterUsuarioAsync(login);

        if (usuario is null) return string.Empty;

        var claims = ObterClaimsUsuario(usuario);
        var tokenDescriptor = CriarTokenDescriptor(key, claims);
        var token = tokenHandler.CreateToken(tokenDescriptor);
            
        return tokenHandler.WriteToken(token);
    }

    private static SecurityTokenDescriptor CriarTokenDescriptor(byte[] key, IEnumerable<Claim> claims)
        => new()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(3),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };

    private static IEnumerable<Claim> ObterClaimsUsuario(Usuario usuario)
    {
        var permissoes = usuario.Pessoa.Perfil.Permissoes.Select(s => s.Permissao.Valor) ?? [];
        var claims = new List<Claim> { new (ClaimTypes.Email, usuario.Email), new (ClaimTypes.Name, usuario.Email) };
        claims.AddRange(permissoes.Select(s => new Claim(ClaimTypes.Role, s.ToString())));

        return claims.ToList();
    }

    private async Task<Usuario?> ObterUsuarioAsync(Login login)
    {
        if (!string.IsNullOrEmpty(login.Crm))
            return await ObterUsuarioAsync(login.Crm, login.Password);
        
        return await ObterUsuarioAsync(login.Email, login.Documento, login.Password);
    }

    private async Task<Usuario?> ObterUsuarioAsync(string email, string documento, string password)
    {
        var encrypt = EncryptPassword(password);
        var query = !string.IsNullOrEmpty(email)
            ? _unitOfWork.Usuario.ObterUsuarioPorFiltroAsync(p => p.Email.Equals(email) && p.Senha.Equals(encrypt))
            : _unitOfWork.Usuario.ObterUsuarioPorFiltroAsync(p =>
                p.Pessoa.Documento.Equals(documento) && p.Senha.Equals(encrypt));
        return await query;
    }

    private async Task<Usuario?> ObterUsuarioAsync(string crm, string password)
    {
        var encrypt = EncryptPassword(password);
        return await _unitOfWork.Usuario.ObterUsuarioPorFiltroAsync(u =>
            u.Pessoa.Medico.Crm.Equals(crm) && u.Senha.Equals(encrypt));
    }

    private string EncryptPassword(string password)
        => BCrypt.Net.BCrypt.HashPassword(password, _credentialSettings.Salt);
}