using System.Collections.Generic;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface INotification
    {
        void SendInvitation( string routeId, List<User> receiver);
        void SendReminder(string routeId);
    }
}
