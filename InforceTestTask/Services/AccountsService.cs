using InforceTestTask.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace InforceTestTask.Services;

public class AccountsService : IAccountsService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    public AccountsService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<bool> LoginAsync(string userName, string password, bool rememberMe)
    {
        var result = await _signInManager.PasswordSignInAsync(userName, password, rememberMe, false);

        if (result.Succeeded)
        {
            return true;
        }

        return false;
    }

    public async Task<IEnumerable<IdentityError>> RegisterAsync(string userName, string password)
    {
        var user = new IdentityUser()
        {
            UserName = userName,
            Email = userName
        };

        var result = await _userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, isPersistent: false);
            return null!;
        }

        return result.Errors;
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}
