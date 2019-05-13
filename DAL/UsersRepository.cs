using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebModels;

namespace DAL
{
    public class UsersRepository : Repository<User>
    {

        public UsersRepository(List<User> _context) : base(_context)
        {
        }


    }
}
