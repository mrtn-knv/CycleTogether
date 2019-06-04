using System.Collections.Generic;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IEquipmentRetriever
    {
        IEnumerable<Equipment> GetAll();
    }
}
