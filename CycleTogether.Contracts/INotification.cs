using System.Collections.Generic;

namespace CycleTogether.Contracts
{
    public interface INotification
    {
        string SendNotification(string notification, string routeId, List<string> receiver);
        string SendReminder(string notification, string routeId);
    }
}
