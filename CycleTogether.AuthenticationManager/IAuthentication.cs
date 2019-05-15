using WebModels;

namespace CycleTogether.AuthenticationManager
{
    public interface IAuthentication
    {
        string Authenticate(string email, string password);
        UserWeb Register(UserWeb user);
    }
}
