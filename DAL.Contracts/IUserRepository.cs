using DAL.Models;

namespace DAL.Contracts
{
    public interface IUserRepository : IRepository<UserEntry>
    {
        UserEntry GetByEmail(string email);       
    }
}
