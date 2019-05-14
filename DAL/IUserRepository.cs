using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);       
    }
}
