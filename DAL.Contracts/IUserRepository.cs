﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        User GetByEmail(string email);
    }
}
