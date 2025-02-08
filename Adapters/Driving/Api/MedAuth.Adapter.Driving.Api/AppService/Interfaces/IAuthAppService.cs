using MedAuth.Adapter.Driving.Api.Dtos;

namespace MedAuth.Adapter.Driving.Api.AppService.Interfaces;

public interface IAuthAppService
{
    Task<string> ObterTokenAsync(LoginViewModel loginViewModel);
}