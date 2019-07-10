using DAL.Contracts;
using DAL.Data;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class UserEquipmentsRepository : DbRepository<UserEquipmentEntry>, IUserEquipmentRepository
    {
        public UserEquipmentsRepository(CycleTogetherDbContext context) : base(context)
        {

        }
        
    }
}
