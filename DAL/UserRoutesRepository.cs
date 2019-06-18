using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class UserRoutesRepository : DbRepository<UserRoute>
    {
        public UserRoutesRepository(CycleTogetherDbContext context) : base(context)
        {

        }
    }
}
