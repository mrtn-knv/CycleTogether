using CycleTogether.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CycleTogether.RoutesSubscriber
{
    public class Compatibility
    {
        public Terrain Terrain { get; private set; }
        public Difficulty Difficulty { get; private set; }
        public Endurance Endurance { get; private set; }
        public Equipment Equipments { get; set; }
        public Compatibility(Terrain terrain, Difficulty difficulty, Endurance endurance, Equipment equipments)
        {
            Terrain = terrain;
            Difficulty = difficulty;
            Endurance = endurance;
            Equipments = equipments;

        }

        public override bool Equals(object obj)
        {
            Compatibility tde = obj as Compatibility;
            if (tde != null)
            {
                return Terrain == tde.Terrain && Difficulty == tde.Difficulty && Endurance == tde.Endurance;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private IEnumerable<Equipment> GetFlags(Equipment input)
        {
            foreach (Equipment value in Equipment.GetValues(input.GetType()))
                if (input.HasFlag(value))
                    yield return value;
        }
    }
}
