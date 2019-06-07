using CycleTogether.Contracts;
using CycleTogether.Enums;
using System.Collections.Generic;
using WebModels;

namespace CycleTogether.RoutesDifficultyManager
{
    public class BeginnerDifficulty : IDifficultyLevel
    {
        private readonly List<TerrainEndurance> criteria = new List<TerrainEndurance>()
        {
            new TerrainEndurance(Terrain.Flat, Endurance.fourHours),
            new TerrainEndurance(Terrain.Ragged, Endurance.fourHours)
        };
        public bool IsTrueFor(Route route)
        {
            if (route.Terrain == Terrain.Extreme)
                return false;
            if (route.Endurance != Endurance.fourHours)
                return false;

            return Metric.MatchByTerrainEndurance(route, criteria);
        }
    }
}
