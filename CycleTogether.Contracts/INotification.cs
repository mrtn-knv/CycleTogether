using System.Collections.Generic;

namespace CycleTogether.Contracts
{
    public interface INotification
    {
        void SendInvitation( string routeId, List<string> receiver);
        void SendReminder(string routeId);
    }
}
