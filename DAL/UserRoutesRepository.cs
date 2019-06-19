using DAL.Contracts;
using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class UserRoutesRepository : DbRepository<UserRouteEntry>, IUserRouteRepository
    {
        public UserRoutesRepository(CycleTogetherDbContext context) : base(context)
        {

        }

    }
}
