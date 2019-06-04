using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CycleTogether.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebModels;

namespace CycleTogetherWeb.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentController : ControllerBase
    {
        private IEquipmentRetriever _equipment;
        public EquipmentController(IEquipmentRetriever equipment)
        {
            _equipment = equipment;
        }

        [HttpGet("all")]
        public IEnumerable<Equipment> GetAvailableEquipments()
        {
            return _equipment.GetAll();
        }

    }
}