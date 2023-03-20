using Microsoft.AspNetCore.Identity;

namespace InforceTestTask.Services.Interfaces;

public interface IAccountsService
{
    Task<IEnumerable<IdentityError>> RegisterAsync(string userName, string password);
    Task<bool> LoginAsync(string userName, string password, bool rememberMe);
    Task LogoutAsync();
}
