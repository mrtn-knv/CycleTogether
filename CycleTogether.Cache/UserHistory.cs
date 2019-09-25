using AutoMapper;
using CycleTogether.Contracts;
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
        private IMapper _mapper;

        public UserHistory(IDatabase redis, IMapper mapper)
        {
            _redis = redis;
            _mapper = mapper;
        }

        public void AddAll(string userId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RouteView> All(string userId)
        {
            try
            {
              var userHistory = JsonConvert.DeserializeObject<List<Route>>(_redis.StringGet(userId));
                return userHistory.Select(route => _mapper.Map<RouteView>(route));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
