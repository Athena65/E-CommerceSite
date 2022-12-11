using Microsoft.AspNetCore.Identity;

namespace BlazorServerSide.Data
{
    public class User:IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
