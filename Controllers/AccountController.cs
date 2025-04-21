using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    // Inject the AccountService via the interface
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    /// <summary>
    /// Registers a new user and sends a verification email.
    /// </summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] AuthDto model)
    {
        return await _accountService.RegisterAsync(model, Request);
    }

    /// <summary>
    /// Verifies the user's email using a confirmation token.
    /// </summary>
    [HttpGet("verify-email")]
    public async Task<IActionResult> VerifyEmail(string userId, string token)
    {
        return await _accountService.VerifyEmailAsync(userId, token);
    }

    /// <summary>
    /// Logs in the user and returns a JWT token.
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthDto model)
    {
        return await _accountService.LoginAsync(model);
    }
}
