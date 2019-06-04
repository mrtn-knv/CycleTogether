using WebModels;

namespace CycleTogether.Contracts
{
    public interface IDifficultyLevel
    {
        bool IsTrueFor(Route route);
    }
}
