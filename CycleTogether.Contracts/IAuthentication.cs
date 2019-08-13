using WebModels;

namespace CycleTogether.Contracts
{
    public interface IAuthentication
    {
        string Authenticate(string email, string password);
        void Register(User user);
    }
}
