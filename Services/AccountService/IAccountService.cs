public interface IAccountService
{
    Task<IActionResult> RegisterAsync(AuthDto model, HttpRequest request);
    Task<IActionResult> VerifyEmailAsync(string userId, string token);
    Task<IActionResult> LoginAsync(AuthDto model);
}