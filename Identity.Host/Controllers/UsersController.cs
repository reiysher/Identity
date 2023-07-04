using Identity.Domain.Entities;

namespace Identity.Host.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserManager<User> _userManager;

    public UsersController(UserManager<User> userManager)
    {
        _userManager = userManager;

    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        if(request.Password != request.PasswordConfirm)
        {
            return BadRequest();
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            UserName = request.Email,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email,
            NormalizedEmail = request.Email.Normalize(),
            NormalizedUserName = request.Email.Normalize(),
            PhoneNumberConfirmed = true,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        result = await _userManager.AddPasswordAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }
}

// todo: move
public record RegisterRequest(string PhoneNumber, string Email, string Password, string PasswordConfirm);