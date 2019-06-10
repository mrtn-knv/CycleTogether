using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CycleTogether.Contracts;

namespace CycleTogetherWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotification _notificator;

        public NotificationController(INotification notificator)
        {
            _notificator = notificator;
        }

        [HttpPost("Invite")]
        public IActionResult SendInvitation()
        {
            _notificator.SendNotification("martina.kuneva@gmail.com", "Invitation");
            return Ok();
        }

    }
}