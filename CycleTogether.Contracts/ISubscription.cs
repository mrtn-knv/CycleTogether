using DAL.Models;
using System;
using System.Collections.Generic;

namespace CycleTogether.Contracts
{
    public interface ISubscription
    {
        bool Subscribe(Guid userId, Guid routeId);
        bool Unsubscribe(UserRouteEntry userFromRoute);
        List<string> SubscribedEmails(string routeId);

    }
}
