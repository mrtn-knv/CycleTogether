using CycleTogether.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using WebModels;

namespace CycleTogether.RoutesDifficultyManager
{
    public class IntermediateDifficulty : IDifficultyLevel
    {
        private List<TerrainEndurance> criteria = new List<TerrainEndurance>
            {
                new TerrainEndurance(Terrain.Flat, Endurance.fourToSixHours),
                new TerrainEndurance(Terrain.Ragged, Endurance.fourToSixHours),
                new TerrainEndurance(Terrain.Extreme, Endurance.fourHours),
            };

        public bool IsTrueFor(RouteWeb route)
        {
            if (route.Endurance == Endurance.moreThanSixHours)
                return false;

            return Metric.MatchByTerrainEndurance(route, criteria);
        }
    }
}
