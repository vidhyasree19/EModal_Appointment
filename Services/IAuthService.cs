using AuthlApi.Models;
public interface IAuthService
{
    Task<bool> Register(AuthModel model);
    Task<string> Login(AuthModel model);
    Task<List<string>> GetUserRoles(string username); // New method to get user roles
}