using MedAuth.Adapter.Driving.Api.AppService.Interfaces;
using MedAuth.Adapter.Driving.Api.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedAuth.Adapter.Driving.Api.Controllers;

[ApiController]
[Route("Auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthAppService _authAppService;

    public AuthController(IAuthAppService authAppService)
    {
        _authAppService = authAppService;
    }
    
    [HttpPost]
    [AllowAnonymous]
    [Route("/api/v1/auth")]
    public async Task<IActionResult> Auth([FromBody] LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState.Values);

        var token = await _authAppService.ObterTokenAsync(loginViewModel);

        return !string.IsNullOrEmpty(token) ? Ok(new { Token = token }) : NotFound();
    }
}