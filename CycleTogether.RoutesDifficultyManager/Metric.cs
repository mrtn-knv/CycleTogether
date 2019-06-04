using CycleTogether.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using WebModels;

namespace CycleTogether.RoutesDifficultyManager
{
    public static class Metric
    {
        public static bool MatchByTerrainEndurance(Route route, List<TerrainEndurance> criteria)
        {
            TerrainEndurance entered = new TerrainEndurance(route.Terrain, route.Endurance);
            List<TerrainEndurance> terrainEndurances = criteria;

            return terrainEndurances.Contains(entered);
        }
    }
}
