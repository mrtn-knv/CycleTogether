using AutoMapper;
using CycleTogether.Contracts;
using DAL.Contracts;
using System.Collections.Generic;
using WebModels;
using System.Linq;

namespace CycleTogether.Equipments
{
    public class EquipmentRetriever : IEquipmentRetriever
    {
        private readonly IEquipmentsRepository _equipments;
        private readonly IMapper _mapper;
        private readonly IEquipmentCache _equipmentsCache;
        public EquipmentRetriever(IEquipmentsRepository equipments, IMapper mapper, IEquipmentCache equipmentsCache)
        {

            _equipments = equipments;
            _mapper = mapper;
            _equipmentsCache = equipmentsCache;
        }
        public IEnumerable<Equipment> GetAll()
        {
            var equipments = _equipmentsCache.All();
            if (equipments == null)
            {
                var equipment = _equipments.GetAll().Select(e => _mapper.Map<Equipment>(e));
                _equipmentsCache.AddAll(equipment.ToList());
                return equipment;
            }
            return equipments;       
        }
    }
}
