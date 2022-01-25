using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids.Extensions
{
    public static class FloatExtensions
    {
        public static float Percentage(this float value, float percent)
        {
            if (percent < 0f) throw new ArgumentOutOfRangeException("percent");

            return percent * value / 100f;
        }

        public static float RandomizeSign(this float value)
        {
            return value * (Random.value > 0.5f ? 1f : -1f);
        }
    }
}
