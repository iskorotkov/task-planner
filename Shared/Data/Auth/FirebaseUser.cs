using System.Collections.Generic;
using System.Security.Claims;

namespace TaskPlanner.Shared.Data.Auth
{
    public class FirebaseUser
    {
        // ReSharper disable UnusedAutoPropertyAccessor.Global

        public bool EmailVerified { get; set; }
        public bool IsAnonymous { get; set; }
        public string? PhoneNumber { get; set; }
        public string? DisplayName { get; set; }
        public string? Email { get; set; }
        public string? PhotoUrl { get; set; }
        public string? Uid { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        
        // ReSharper restore UnusedAutoPropertyAccessor.Global

        public IEnumerable<Claim> Claims()
        {
            return new List<Claim>
            {
                new Claim(ClaimTypes.Authentication, "true"),
                new Claim(ClaimTypes.Name, Email),
                new Claim(ClaimTypes.Anonymous, IsAnonymous.ToString()),
                new Claim(ClaimTypes.Email, Email)
            };
        }
    }
}
