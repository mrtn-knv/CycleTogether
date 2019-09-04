using CycleTogether.Contracts;
using CycleTogether.Enums;
using System;
using WebModels;

namespace CycleTogether.RoutesDifficultyManager
{
    public class DifficultyCalculator : IDifficultyCalculator
    {
        private readonly IDifficultyLevel _beginnerLevel;
        private readonly IDifficultyLevel _intermediateLevel;
        private readonly IDifficultyLevel _proLevel;

        public DifficultyCalculator()
        {
            _beginnerLevel = new BeginnerDifficulty();
            _intermediateLevel = new IntermediateDifficulty();
            _proLevel = new ProDifficulty();
        }

        public Difficulty DifficultyLevel(Route route)
        {
            if (_beginnerLevel.IsTrueFor(route))
                return Difficulty.Easy;
            else if (_intermediateLevel.IsTrueFor(route))
                return Difficulty.Intermediate;
            else if (_proLevel.IsTrueFor(route))
                return Difficulty.Pro;
            else
                return Difficulty.None;
        }
    }
}
