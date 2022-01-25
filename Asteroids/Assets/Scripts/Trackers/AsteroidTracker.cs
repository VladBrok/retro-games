using System;
using UnityEngine;

namespace Asteroids
{
    public class AsteroidTracker<T> where T : IDestructible
    {
        private readonly ISpawner<T> _spawner;
        private readonly ICenterProvider _spawnOrigin;
        private int _bigAsteroidCount;
        private int _asteroidsLeft;

        public AsteroidTracker(
            ISpawner<T> spawner, 
            ICenterProvider spawnOrigin, 
            int bigAsteroidCount)
        {
            if (bigAsteroidCount < 1) throw new ArgumentOutOfRangeException("bigAsteroidCount");

            _spawner = spawner;
            _spawnOrigin = spawnOrigin;
            _bigAsteroidCount = bigAsteroidCount;
            _asteroidsLeft = GetAsteroidsToDestroy();

            _spawner.Destroyed += OnAsteroidDestroyed;
            _spawner.Spawn(_bigAsteroidCount, _spawnOrigin.Center);
        }

        private void OnAsteroidDestroyed(T obj)
        {
            if (--_asteroidsLeft == 0)
            {
                _spawner.Spawn(++_bigAsteroidCount, _spawnOrigin.Center);
                _asteroidsLeft = GetAsteroidsToDestroy();
            }
        }

        private int GetAsteroidsToDestroy()
        {
            return _bigAsteroidCount * 7;
        }
    }
}
