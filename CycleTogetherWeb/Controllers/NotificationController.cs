using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CycleTogether.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace CycleTogetherWeb.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotification _notificator;

        public NotificationController(INotification notificator)
        {
            _notificator = notificator;
        }
                
        [HttpPost("{routeId}/invite")]
        public void SendInvitation([FromBody]List<string> emails, string routeId)
        {
            _notificator.SendInvitation(routeId, emails);
        }

        [AllowAnonymous]
        [HttpPost("{routeId}/remind")]
        public void Remind(string routeId)
        {
            _notificator.SendReminder(routeId);
        }

    }
}