using AutoMapper;
using CycleTogether.Contracts;
using DAL.Contracts;
using System;
using System.Collections.Generic;
using WebModels;

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
        public IEnumerable<EquipmentWeb> GetAll()
        {
            var all = _equipments.GetAll();
            foreach (var equipment in all)
            {
                yield return _mapper.Map<EquipmentWeb>(equipment);
            }
        }
    }
}
