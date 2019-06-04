using CycleTogether.Contracts;
using CycleTogether.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using WebModels;

namespace CycleTogether.RoutesDifficultyManager
{
    public class ProDifficulty : IDifficultyLevel
    {
        private readonly List<TerrainEndurance> criteria = new List<TerrainEndurance>
            {
                new TerrainEndurance(Terrain.Flat, Endurance.moreThanSixHours),
                new TerrainEndurance(Terrain.Ragged, Endurance.moreThanSixHours),
                new TerrainEndurance(Terrain.Extreme, Endurance.fourToSixHours),
                new TerrainEndurance(Terrain.Extreme, Endurance.moreThanSixHours)
            };

        public bool IsTrueFor(Route route)
        {
            if (route.Endurance == Endurance.fourHours)
                return false;

            return Metric.MatchByTerrainEndurance(route, criteria);
        }
    }
}
