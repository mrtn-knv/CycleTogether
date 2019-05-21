using DAL.Models;

namespace DAL.Contracts
{
    public interface IRouteRepository : IRepository<Route>
    {
        void Subscribe(string email, Route route);
        void Unsubscribe(string email, Route route);
    }
}
