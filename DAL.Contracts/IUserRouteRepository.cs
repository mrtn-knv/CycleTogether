﻿using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Contracts
{
    public interface IUserRouteRepository : IRepository<UserRouteEntry>
    {
        bool Exists(UserRouteEntry userRoute);
    }
}
