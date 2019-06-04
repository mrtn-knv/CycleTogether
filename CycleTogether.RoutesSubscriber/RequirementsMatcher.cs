﻿using CycleTogether.Enums;
using System;
using System.Collections.Generic;

namespace CycleTogether.RoutesSubscriber
{
    public class RequirementsMatcher
    {
        public Terrain Terrain { get; private set; }
        public Difficulty Difficulty { get; private set; }
        public Endurance Endurance { get; private set; }
        List<Guid> Equipments { get; }
        public RequirementsMatcher(Terrain terrain, Difficulty difficulty, Endurance endurance, List<Guid> equipments)
        {
            Terrain = terrain;
            Difficulty = difficulty;
            Endurance = endurance;
            Equipments = equipments;
        }

        public override bool Equals(object obj)
        {
            RequirementsMatcher requirements = obj as RequirementsMatcher;
            if (requirements != null)
            {
                var match =  EquipmentsMatch(requirements);
                return Terrain == requirements.Terrain &&
                       Difficulty == requirements.Difficulty &&
                       Endurance == requirements.Endurance &&
                       match;
            }

            return false;
        }

        private bool EquipmentsMatch(RequirementsMatcher requirements)
        {
            foreach (var equipment in Equipments)
            {
                if (!requirements.Equipments.Contains(equipment))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
