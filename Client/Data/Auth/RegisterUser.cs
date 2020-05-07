using System.ComponentModel.DataAnnotations;

namespace TaskPlanner.Client.Data.Auth
{
    // TODO: Move class to Shared project.
    public class RegisterUser
    {
        [Required, EmailAddress]
        public string? Username { get; set; }

        [Required, DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
