using System;
using System.Collections.Generic;
using System.Text;
using WebModels;

namespace CycleTogether.RoutesDifficultyManager
{
    public interface IDifficultyLevel
    {
        bool IsTrueFor(RouteWeb route);
    }
}
