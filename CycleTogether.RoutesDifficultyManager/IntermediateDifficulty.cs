﻿using CycleTogether.Contracts;
using CycleTogether.Enums;
using System.Collections.Generic;
using WebModels;

namespace CycleTogether.RoutesDifficultyManager
{
    public class IntermediateDifficulty : IDifficultyLevel
    {
        private readonly List<TerrainEndurance> criteria = new List<TerrainEndurance>
            {
                new TerrainEndurance(Terrain.Flat, Endurance.fourToSixHours),
                new TerrainEndurance(Terrain.Ragged, Endurance.fourToSixHours),
                new TerrainEndurance(Terrain.Extreme, Endurance.fourHours),
            };

        public bool IsTrueFor(Route route)
        {
            if (route.Endurance == Endurance.moreThanSixHours)
                return false;

            return Metric.MatchByTerrainEndurance(route, criteria);
        }
    }
}
