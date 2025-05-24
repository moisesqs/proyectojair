using Microsoft.AspNetCore.Mvc;
using SlaveWorkColab.Api.Interfaces;
using SlaveWorkColab.Core.Dto;
using TecPurisima.Ecommerce.Api.Services.Interfaces;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IUsersRepository _userRepository;

    private readonly IUsersService _userService;

    public AuthController(IUsersService userService)
    {
        _userService = userService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _userService.AuthenticateAsync(dto.Email, dto.Password);

        if (user == null)
        {
            return Unauthorized(new { message = "Email o Password incorrectos." });
        }

        return Ok(new
        {
            message = "Successful Login",
            userId = user.Id,
            username = user.Username
        });
    }

}