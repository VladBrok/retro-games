using System;
using System.Collections.Generic;
using UnityEngine;

namespace Asteroids
{
    public class AsteroidController<T> where T : IDestructible, ICenterProvider
    {
        private readonly Dictionary<AsteroidType, IPool<T>> _pools;
        private readonly ICenterProvider _spawnOrigin;
        private int _bigAsteroidCount;

        public AsteroidController(
            Dictionary<AsteroidType, IPool<T>> pools, 
            ICenterProvider spawnOrigin, 
            int bigAsteroidCount)
        {
            if (bigAsteroidCount < 1) throw new ArgumentOutOfRangeException("bigAsteroidCount");

            _pools = pools;
            _spawnOrigin = spawnOrigin;
            _bigAsteroidCount = bigAsteroidCount;

            foreach (var pair in _pools)
            {
                pair.Value.ObjectDestroyed += a => OnAsteroidDestroyed(a, pair.Key);
            }
            SpawnBigAsteroids();
        }

        public int AsteroidsLeft { get; private set; }

        private void OnAsteroidDestroyed(T asteroid, AsteroidType type)
        {
            Split(asteroid, type);
            if (--AsteroidsLeft == 0)
            {
                ++_bigAsteroidCount;
                SpawnBigAsteroids();
            }
        }

        private void Split(T asteroid, AsteroidType type)
        {
            switch (type)
            {
                case AsteroidType.Big:
                    _pools[AsteroidType.Medium].Get(asteroid.Center);
                    _pools[AsteroidType.Medium].Get(asteroid.Center);
                    break;
                case AsteroidType.Medium:
                    _pools[AsteroidType.Small].Get(asteroid.Center);
                    _pools[AsteroidType.Small].Get(asteroid.Center);
                    break;
            }
        }

        private void SpawnBigAsteroids()
        {
            _pools[AsteroidType.Big].Get(_bigAsteroidCount, _spawnOrigin.Center);
            AsteroidsLeft = _bigAsteroidCount * 7;
        }
    }
}
