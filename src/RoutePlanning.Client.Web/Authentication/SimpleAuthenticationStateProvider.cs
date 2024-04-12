using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace RoutePlanning.Client.Web.Authentication;

public sealed class SimpleAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ProtectedBrowserStorage browserStorage;
    private readonly AuthenticationState anonymousState;

    public SimpleAuthenticationStateProvider(ProtectedBrowserStorage browserStorage)
    {
        this.browserStorage = browserStorage;
        anonymousState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var userSessionStorageResult = await browserStorage.GetAsync<UserSession>(nameof(UserSession));

        var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;

        if (userSession is null)
        {
            return anonymousState;
        }

        return CreateUserAuthenticationState(userSession);
    }

    public async Task SetAuthenticationStateAsync(UserSession userSession)
    {
        await browserStorage.SetAsync(nameof(UserSession), userSession);

        var userAuthState = CreateUserAuthenticationState(userSession);
        NotifyAuthenticationStateChanged(Task.FromResult(userAuthState));
    }

    public async Task ClearAuthenticationStateAsync()
    {
        await browserStorage.DeleteAsync(nameof(UserSession));

        NotifyAuthenticationStateChanged(Task.FromResult(anonymousState));
    }

    private static AuthenticationState CreateUserAuthenticationState(UserSession userSession)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userSession.Username),
        };

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "UsernamePassword"));

        return new AuthenticationState(claimsPrincipal);
    }
}
