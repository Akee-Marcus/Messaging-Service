namespace ChatService.Models
{
    public class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty; // plain text just for this assignment
    }

    // DTO for sign in request
    public class SignInRequest
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
