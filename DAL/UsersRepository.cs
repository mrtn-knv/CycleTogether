﻿using DAL.Contracts;
using DAL.Models;
using System.Linq;
using DAL.Data;

namespace DAL
{
    public class UsersRepository : DbRepository<UserEntry>, IUserRepository
    {        
        public UsersRepository(CycleTogetherDbContext context) : base(context)
        {
         
        }

        public UserEntry GetByEmail(string email)
        {
            return GetAll().FirstOrDefault(u => u.Email == email);
        }

        

    }
}
