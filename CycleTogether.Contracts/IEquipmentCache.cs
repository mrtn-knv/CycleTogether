using System;
using System.Collections.Generic;
using System.Text;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IEquipmentCache 
    {
        IEnumerable<Equipment> All();
        IEnumerable<Equipment> AllBy(string id);
        void AddAll(List<Equipment> equipments);
        void Add(Equipment equipment);
        void Remove(Equipment equipment);
    }
}
