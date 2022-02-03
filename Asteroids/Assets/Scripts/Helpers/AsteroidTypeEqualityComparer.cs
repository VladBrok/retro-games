using System.Collections.Generic;

namespace Asteroids
{
    public class AsteroidTypeEqualityComparer : IEqualityComparer<AsteroidType>
    {
        public bool Equals(AsteroidType x, AsteroidType y)
        {
            return x == y;
        }

        public int GetHashCode(AsteroidType obj)
        {
            return obj.GetHashCode();
        }
    }
}
