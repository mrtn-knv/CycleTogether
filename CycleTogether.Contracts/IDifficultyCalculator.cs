using CycleTogether.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IDifficultyCalculator
    {
        Difficulty DifficultyLevel(Route route);
    }
}
