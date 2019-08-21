using System.Collections.Generic;
using System.Data;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface ISearchManager
    {
        void CreateIndex();
        void AddRouteToIndex(Route route);
        void RemoveFromIndex(Route route);
        void UpdateIndex();        
    }
}
