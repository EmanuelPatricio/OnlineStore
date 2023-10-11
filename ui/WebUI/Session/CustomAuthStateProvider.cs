using Application.Models.Requests;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Text.Json;
using WebUI.Helpers;
using WebUI.HttpClientUtilities;

namespace WebUI.Session;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly SessionHelper _session;
    private readonly JwtApplication _jwtApplication;
    private readonly ApiHttpClient _apiHttpClient;

    public CustomAuthStateProvider(SessionHelper session, JwtApplication jwtApplication, ApiHttpClient apiHttpClient)
    {
        _session = session;
        _jwtApplication = jwtApplication;
        _apiHttpClient = apiHttpClient;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return Task.Run(GetAuthenticationState);
    }

    private async Task<AuthenticationState> GetAuthenticationState()
    {
        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);
        var state = new AuthenticationState(user);

        if (_session is null)
        {
            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }

        var loginReq = await _apiHttpClient.SendRequest<UserLoginReq>(HttpMethod.Post, ApiRoutes.Login.login, _session, false);

        if (loginReq is null)
        {
            NotifyAuthenticationStateChanged(Task.FromResult(state));
            return state;
        }

        _session.Id = loginReq.Id;
        _jwtApplication.JwtToken = loginReq.JwtToken;

        if (!string.IsNullOrEmpty(_jwtApplication.JwtToken))
        {
            identity = new ClaimsIdentity(ParseClaimsFromJwt(_jwtApplication.JwtToken), "jwt");
        }

        user = new ClaimsPrincipal(identity);
        state = new AuthenticationState(user);

        NotifyAuthenticationStateChanged(Task.FromResult(state));

        return state;
    }

    private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var payload = jwt.Split('.')[1];
        var jsonBytes = ParseBase64WithoutPadding(payload);
        var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        return keyValuePairs?.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!))
               ?? throw new InvalidOperationException();
    }

    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }
}
