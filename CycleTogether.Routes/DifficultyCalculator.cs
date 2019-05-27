using CycleTogether.Enums;
using CycleTogether.RoutesDifficultyManager;
using System;
using System.Collections.Generic;
using WebModels;

namespace CycleTogether.Routes
{
    public class DifficultyCalculator
    {
        private IDifficultyLevel beginnerLevel;
        private IDifficultyLevel intermediateLevel;
        private IDifficultyLevel proLevel;

        public DifficultyCalculator()
        {
            this.beginnerLevel = new BeginnerDifficulty();
            this.intermediateLevel = new IntermediateDifficulty();
            this.proLevel = new ProDifficulty();
        }

        public Difficulty DifficultyLevel(RouteWeb route)
        {
            if (this.beginnerLevel.IsTrueFor(route))
                return Difficulty.Easy;
            else if (this.intermediateLevel.IsTrueFor(route))
                return Difficulty.Intermediate;
            else if (this.proLevel.IsTrueFor(route))
                return Difficulty.Pro;
            else
                throw new ArgumentException("Invalid difficulty level stage");
        }
    }

    //interface DifficultyLevel
    //{
    //    bool IsTrueFor(RouteWeb route);
    //}

    //class ProDifficulty : DifficultyLevel
    //{
    //}

    //class IntermediateDifficulty : DifficultyLevel
    //{   
    //    public bool IsTrueFor(RouteWeb route)
    //    {
    //        return MatchByTerrainEnduranceMetric(route);            
    //    }
    //    private List<TerrainEndurance> TerrainEndurances()
    //    {
    //        return new List<TerrainEndurance>
    //        {
    //            new TerrainEndurance(Terrain.Flat, Endurance.fourToSixHours),
    //            new TerrainEndurance(Terrain.Ragged, Endurance.fourToSixHours),
    //            new TerrainEndurance(Terrain.Extreme, Endurance.fourHours),
    //        };
    //    }
    //    private bool MatchByTerrainEnduranceMetric(RouteWeb route)
    //    {
    //        if (route.Endurance == Endurance.moreThanSixHours)
    //            return false;

    //        TerrainEndurance entered = new TerrainEndurance(route.Terrain, route.Endurance);
    //        List<TerrainEndurance> beginnerTerrainEndurances = this.TerrainEndurances();

    //        return beginnerTerrainEndurances.Contains(entered);
    //    }
    //}

    

    //class TerrainEndurance
    //{
    //    public TerrainEndurance(Terrain t, Endurance e)
    //    {
    //        this.Terrain = t;
    //        this.Endurance = e;
    //    }

    //    public Terrain Terrain { get; private set; }
    //    public Endurance Endurance { get; private set; }

    //    public override bool Equals(object obj)
    //    {
    //        TerrainEndurance o = obj as TerrainEndurance;

    //        if (o != null)
    //        {
    //            return this.Terrain == o.Terrain && this.Endurance == o.Endurance;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }

    //    public override int GetHashCode()
    //    {
    //        return base.GetHashCode();
    //    }
    //}
}
