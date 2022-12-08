using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ClientSide.AuthProviders
{
    public class TestAuthStateProvider:AuthenticationStateProvider
    {
        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            await Task.Delay(1500);
            var claims=new List<Claim>
            {
                new Claim(ClaimTypes.Name,"Athena Farris"),
                new Claim(ClaimTypes.Role,"Administrator")
            };
            var anonymous = new ClaimsIdentity();
            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonymous)));  
        }
    }
}
