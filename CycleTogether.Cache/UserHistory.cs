using AutoMapper;
using CycleTogether.Contracts;
using DAL.Contracts;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebModels;

namespace Cache
{
    public class UserHistory : IUserHistoryCache
    {
        private readonly IDatabase _redis;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _db;
        private const string key = "history_";

        public UserHistory(IDatabase redis, IMapper mapper, IUnitOfWork db)
        {
            _redis = redis;
            _mapper = mapper;
            _db = db;
        }

        public void AddAll(string userId)
        {
            var subscriptions = _db.UserRoutes.GetAll()
                    .Where(ur => ur.UserId == Guid.Parse(userId))
                    .Select(ur => ur.Route)
                    .Select(_mapper.Map<Route>);
            var history = subscriptions.Where(route => DateTime.Compare(DateTime.UtcNow, route.StartTime) > 0);
            _redis.StringSet(key + userId, JsonConvert.SerializeObject(history.ToList()));
        }

        public IEnumerable<RouteView> All(string userId)
        {
            try
            {
                var userHistory = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(userId));
                return userHistory.Select(route => _mapper.Map<RouteView>(route));
            }
            catch (ArgumentException)
            {
                this.AddAll(userId);
                return JsonConvert.DeserializeObject<List<RouteView>>(_redis.StringGet(key + userId))
                       .Select(r => _mapper.Map<RouteView>(r));
            }
        }
    }
}
