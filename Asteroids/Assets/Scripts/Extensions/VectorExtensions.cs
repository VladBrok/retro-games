using UnityEngine;

namespace Asteroids.Extensions
{
    public static class VectorExtensions
    {
        public static Vector2 RandomDirection(this Vector2 _)
        {
            return new Vector2(Random.value.RandomizeSign(),
                               Random.value.RandomizeSign()).normalized;
        }
    }
}
