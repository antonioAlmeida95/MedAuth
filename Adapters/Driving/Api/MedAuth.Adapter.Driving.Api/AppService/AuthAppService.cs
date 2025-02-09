using AutoMapper;
using MedAuth.Adapter.Driving.Api.AppService.Interfaces;
using MedAuth.Adapter.Driving.Api.Dtos;
using MedAuth.Core.Domain.Entities;
using MedAuth.Core.Domain.Services;

namespace MedAuth.Adapter.Driving.Api.AppService;

public class AuthAppService : IAuthAppService
{
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;

    public AuthAppService(IMapper mapper, IAuthService authService)
    {
        _mapper = mapper;
        _authService = authService;
    }
    
    public async Task<string> ObterTokenAsync(LoginViewModel loginViewModel)
    {
        var model = _mapper.Map<Login>(loginViewModel);
        return await _authService.GenerateToken(model);
    }
}