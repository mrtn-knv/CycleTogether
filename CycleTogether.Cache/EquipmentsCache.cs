using CycleTogether.Contracts;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using WebModels;

namespace CycleTogether.Cache
{
    public class EquipmentsCache : IEquipmentCache
    {
        private const string key = "equipments";
        private readonly IDatabase _redis;
        public EquipmentsCache(IDatabase redis)
        {
            _redis = redis;
        }

        public void AddItem(Equipment item)
        {
            if (item != null)
            {
                var equipments = JsonConvert.DeserializeObject<List<Equipment>>(_redis.StringGet(key));
                equipments.Add(item);
                _redis.StringSet(key, JsonConvert.SerializeObject(equipments));
            }
        }

        public void AddAll(List<Equipment> items)
        {
            if (items.Count > 0)
                _redis.StringSet(key, JsonConvert.SerializeObject(items));
            
        }

        public IEnumerable<Equipment> All()
        {
            try
            {
                return JsonConvert.DeserializeObject<IEnumerable<Equipment>>(_redis.StringGet(key));
            }
            catch (ArgumentNullException ex)
            {
                //TODO: logger for exception
                
                return null; 
            }
        }

        public IEnumerable<Equipment> AllBy(string id)
        {
            return JsonConvert.DeserializeObject<IEnumerable<Equipment>>(_redis.StringGet(key + id));
        }

        public void RemoveItem(Equipment item)
        {
            if (item != null)
            {
                var equipments = JsonConvert.DeserializeObject<List<Equipment>>(_redis.StringGet(key));
                equipments.Remove(item);
            }            
        }

        public Equipment GetItem(string id)
        {
            throw new NotImplementedException();
        }
    }
}
