using CycleTogether.Enums;

namespace CycleTogether.RoutesDifficultyManager
{
    public class TerrainEndurance
    {
        public TerrainEndurance(Terrain t, Endurance e)
        {
            this.Terrain = t;
            this.Endurance = e;
        }

        public Terrain Terrain { get; private set; }
        public Endurance Endurance { get; private set; }

        public override bool Equals(object obj)
        {
            TerrainEndurance o = obj as TerrainEndurance;

            if (o != null)
            {
                return this.Terrain == o.Terrain && this.Endurance == o.Endurance;
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
