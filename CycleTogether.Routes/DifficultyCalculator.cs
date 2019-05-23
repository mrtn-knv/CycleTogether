using CycleTogether.Enums;
using System;
using WebModels;

namespace CycleTogether.Routes
{
    public class DifficultyCalculator
    {
        public DifficultyCalculator()
        {

        }

        public void Set(RouteWeb route)
        {
            if (IsBeginner(route))
            {
                route.Difficulty = Difficulty.Easy;
            }
            if (IsIntermediate(route))
            {
                route.Difficulty = Difficulty.Intermediate;
            }
            if (IsPro(route))
            {
                route.Difficulty = Difficulty.Pro;
            }
        }

        private bool IsPro(RouteWeb route)
        {
            if (route.Endurance == Endurance.moreThanSixHours &&
                route.Terrain == Terrain.Ragged)
            {
                return true;
            }
            else if (route.Endurance == Endurance.fourToSixHours &&
                     route.Terrain == Terrain.Extreme)
            {
                return true;
            }
            else if (route.Endurance == Endurance.moreThanSixHours &&
                     route.Terrain == Terrain.Flat)
            {
                return true;
            }
            else if (route.Endurance == Endurance.moreThanSixHours &&
                     route.Terrain == Terrain.Extreme)
            {
                return true;
            }
            return false;
        }

        private bool IsIntermediate(RouteWeb route)
        {
            if (route.Endurance == Endurance.fourToSixHours &&
                route.Terrain == Terrain.Ragged)
            {
                return true;
            }
            else if (route.Endurance == Endurance.fourToSixHours &&
                     route.Terrain == Terrain.Flat)
            {
                return true;
            }
            else if (route.Endurance == Endurance.fourHours &&
                     route.Terrain == Terrain.Extreme)
            {
                return true;
            }
            else if (route.Endurance == Endurance.fourToSixHours &&
                     route.Terrain == Terrain.Ragged)
            {
                return true;
            }

            return false;
        }

        private bool IsBeginner(RouteWeb route)
        {

            if (route.Endurance == Endurance.fourHours &&
                   route.Terrain == Terrain.Flat)
            {
                return true;
            }
            else if (route.Terrain == Terrain.Ragged &&
                     route.Endurance == Endurance.fourHours)
            {
                return true;
            }

            return false;
        }
    }
}
