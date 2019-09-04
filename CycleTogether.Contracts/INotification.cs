using System.Collections.Generic;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface INotification
    {
        void SendInvitation( string routeId, List<string> receiver);
        void SendReminder(string routeId);
    }
}
