using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Asteroids
{
    public static class IEnumerableExtensions
    {
        public static T TakeRandom<T>(this IEnumerable<T> source)
        {
            return source.ElementAt(Random.Range(0, source.Count()));
        }
    }
}
