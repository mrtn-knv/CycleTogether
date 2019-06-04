using System.Collections.Generic;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IEquipmentManager
    {
        IEnumerable<EquipmentWeb> GetAll();
    }
}
