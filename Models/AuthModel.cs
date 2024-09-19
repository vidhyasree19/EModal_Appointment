namespace AuthlApi.Models
{
public class AuthModel
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Role { get; set; } // Optional for registration
}
}