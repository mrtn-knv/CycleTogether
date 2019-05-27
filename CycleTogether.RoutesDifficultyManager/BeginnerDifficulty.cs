using CycleTogether.Enums;
using System;
using System.Collections.Generic;
using WebModels;

namespace CycleTogether.RoutesDifficultyManager
{
    public class BeginnerDifficulty : IDifficultyLevel
    {
        private List<TerrainEndurance> criteria = new List<TerrainEndurance>()
        {
            new TerrainEndurance(Terrain.Flat, Endurance.fourHours),
            new TerrainEndurance(Terrain.Ragged, Endurance.fourHours)
        };
        public bool IsTrueFor(RouteWeb route)
        {
            if (route.Terrain == Terrain.Extreme)
                return false;
            if (route.Endurance != Endurance.fourHours)
                return false;

            return Metric.MatchByTerrainEndurance(route, criteria);
        }

        //private bool MatchByTerrainEnduranceMetric(RouteWeb route)
        //{
        //    TerrainEndurance entered = new TerrainEndurance(route.Terrain, route.Endurance);
        //    return criteria.Contains(entered);
        //}


    }
}
