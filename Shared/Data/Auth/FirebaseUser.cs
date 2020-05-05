namespace TaskPlanner.Shared.Data.Auth
{
    public class FirebaseUser
    {
        public bool EmailVerified { get; set; }
        public bool IsAnonymous { get; set; }
        public string? PhoneNumber { get; set; }
        public string? DisplayName { get; set; }
        public string? Email { get; set; }
        public string? PhotoUrl { get; set; }
        public string? ProviderId { get; set; }
        public string? Uid { get; set; }
    }
}
