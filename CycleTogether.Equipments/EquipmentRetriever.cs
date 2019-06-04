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
        public EquipmentRetriever(IEquipmentsRepository equipments, IMapper mapper)
        {
            _equipments = equipments;
            _mapper = mapper;
        }
        public IEnumerable<Equipment> GetAll()
        {            
            return _equipments.GetAll().Select(equipment => _mapper.Map<Equipment>(equipment));            
        }
    }
}
