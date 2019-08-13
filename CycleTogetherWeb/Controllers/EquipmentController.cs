using System.Collections.Generic;
using CycleTogether.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebModels;

namespace CycleTogetherWeb.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentRetriever _equipment;
        public EquipmentController(IEquipmentRetriever equipment)
        {
            _equipment = equipment;
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public IEnumerable<Equipment> GetAvailableEquipments()
        {
            return _equipment.GetAll();
        }

    }
}