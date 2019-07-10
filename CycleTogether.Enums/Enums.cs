using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Runtime.Serialization;

namespace CycleTogether.Enums
{
    public enum Terrain
    {
        None,
        Flat,
        Ragged,
        Extreme
    }

    public enum TypeOfRoute
    {
        None,
        Family,
        Classic,
        MountainBike,
    }

    public enum Difficulty
    {
        None,
        Easy,
        Intermediate,
        Pro
    }

    public enum Endurance
    {
        None,
        fourHours,
        fourToSixHours,
        moreThanSixHours
    }
}
