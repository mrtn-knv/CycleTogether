using System;
using System.Collections.Generic;
using System.Text;
using WebModels;

namespace CycleTogether.Contracts
{
    public interface IDifficultyLevel
    {
        bool IsTrueFor(Route route);
    }
}
