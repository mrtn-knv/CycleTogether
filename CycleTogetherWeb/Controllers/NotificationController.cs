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
        public IActionResult SendInvitation([FromBody]List<string> emails, string notificationType, string routeId)
        {            
            return Content(_notificator.SendNotification(notificationType, routeId, emails));
        }

        [AllowAnonymous]
        [HttpPost("{routeId}/remind")]
        public IActionResult Remind(string notificationType, string routeId)
        {
            return Content(_notificator.SendReminder(notificationType, routeId));
        }

    }
}