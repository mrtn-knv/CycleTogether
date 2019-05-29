using System;

namespace CycleTogether.Enums
{
    public enum Terrain
    {
        Ragged,
        Flat,
        Extreme
    }

    public enum TypeOfRoute
    {
        MountainBike,
        Family,
        Classic,
        FreeForAll
    }

    public enum Difficulty
    {
        Easy,
        Intermediate,
        Pro
    }
    public enum Endurance
    {
        fourHours,
        fourToSixHours,
        moreThanSixHours
    }

    [Flags]
    public enum Equipment
    {
        None = 0,
        Helmet = 1,
        Naxers = 2,
        Wristguards = 4,
        KneePads = 8
    }
}
