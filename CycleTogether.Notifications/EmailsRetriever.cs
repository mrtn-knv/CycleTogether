using AutoMapper;
using DAL.Contracts;
using DAL.Models;
using System.Collections.Generic;
using WebModels;

namespace CycleTogether.Notification
{
    public class EmailsRetriever
    {
        private readonly IUserRepository _users;
        private readonly IMapper _mapper;

        public EmailsRetriever(IUserRepository users, IMapper mapper)
        {
            _users = users;
            _mapper = mapper;
        }

        public IEnumerable<string> GetUsersFromRoute(Route route)
        {
            var userRoute = _mapper.Map<RouteEntry>(route).UserRoutes;
            foreach (var entry in userRoute)
            {
               var user = _users.GetById(entry.UserId);
               yield return user.Email;
            }
        }
    }
}
