﻿using CycleTogether.Enums;
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
}
