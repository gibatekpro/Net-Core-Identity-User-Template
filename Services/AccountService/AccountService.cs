using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

public class AccountService : IAccountService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IEmailService _emailService;
    private readonly JwtSettings _jwtSettings;
    private readonly EmailSettings  _emailSettings;

    public AccountService(UserManager<IdentityUser> userManager,
                          SignInManager<IdentityUser> signInManager,
                          IEmailService emailService,
                          IOptions<AppConfig> config)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _emailService = emailService;
        _jwtSettings = config.Value.JwtSettings;
        _emailSettings = config.Value.EmailSettings;
    }

    public async Task<IActionResult> RegisterAsync(AuthDto model, HttpRequest request)
    {
        var user = new IdentityUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var verificationLink = request.Scheme + "://" + request.Host + "/api/account/verify-email?userId=" + user.Id + "&token=" + Uri.EscapeDataString(token);

            await _emailService.SendEmailAsync(user.Email, "Verify Email", $"Click to verify: {verificationLink}");
            return new OkObjectResult("User created. Verification email sent.");
        }

        return new BadRequestObjectResult(result.Errors);
    }

    public async Task<IActionResult> VerifyEmailAsync(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return new NotFoundObjectResult("User not found.");

        var result = await _userManager.ConfirmEmailAsync(user, token);
        return result.Succeeded ? new OkObjectResult("Email verified.") : new BadRequestObjectResult("Verification failed.");
    }

    public async Task<IActionResult> LoginAsync(AuthDto model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
        if (!result.Succeeded) return new UnauthorizedObjectResult("Invalid login.");

        var user = await _userManager.FindByEmailAsync(model.Email);
        var roles = await _userManager.GetRolesAsync(user);
        var token = GenerateJwtToken(user, roles);
        return new OkObjectResult(new { Token = token });
    }

    private string GenerateJwtToken(IdentityUser user, IList<string> roles)
{
    var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    foreach (var role in roles)
    {
        claims.Add(new Claim(ClaimTypes.Role, role));
    }

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: _jwtSettings.Issuer,
        audience: _jwtSettings.Issuer,
        claims: claims,
        expires: DateTime.UtcNow.AddHours(_jwtSettings.ExpireHours),
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}

}