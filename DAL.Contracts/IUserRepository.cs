using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Contracts
{
    public interface IUserRepository : IRepository<UserEntry>
    {
        UserEntry GetByEmail(string email);
        //void AddRoute(RouteEntry route, Guid id);        
    }
}
