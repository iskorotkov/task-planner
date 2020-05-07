using System.ComponentModel.DataAnnotations;

namespace TaskPlanner.Shared.Data.Auth
{
    public class SignInUser
    {
        // ReSharper disable UnusedAutoPropertyAccessor.Global
        
        [Required, EmailAddress] public string? Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }
        
        // ReSharper restore UnusedAutoPropertyAccessor.Global
    }
}
