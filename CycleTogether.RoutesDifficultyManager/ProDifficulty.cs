using CycleTogether.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using WebModels;

namespace CycleTogether.RoutesDifficultyManager
{
    public class ProDifficulty : IDifficultyLevel
    {
        private List<TerrainEndurance> criteria = new List<TerrainEndurance>
            {
                new TerrainEndurance(Terrain.Flat, Endurance.moreThanSixHours),
                new TerrainEndurance(Terrain.Ragged, Endurance.moreThanSixHours),
                new TerrainEndurance(Terrain.Extreme, Endurance.fourToSixHours),
                new TerrainEndurance(Terrain.Extreme, Endurance.moreThanSixHours)
            };

        public bool IsTrueFor(RouteWeb route)
        {
            if (route.Endurance == Endurance.fourHours)
                return false;

            return Metric.MatchByTerrainEndurance(route, criteria);
        }

        //public bool IsTrueFor(RouteWeb route)
        //{
        //    if (route.Endurance == Endurance.moreThanSixHours &&
        //        route.Terrain == Terrain.Ragged)
        //    {
        //        return true;
        //    }
        //    else if (route.Endurance == Endurance.fourToSixHours &&
        //             route.Terrain == Terrain.Extreme)
        //    {
        //        return true;
        //    }
        //    else if (route.Endurance == Endurance.moreThanSixHours &&
        //             route.Terrain == Terrain.Flat)
        //    {
        //        return true;
        //    }
        //    else if (route.Endurance == Endurance.moreThanSixHours &&
        //             route.Terrain == Terrain.Extreme)
        //    {
        //        return true;
        //    }
        //    return false;
        //}
    }
}
