using System;
using UnityEngine;

namespace Asteroids
{
    public class AsteroidController<T> where T : IDestructible
    {
        private readonly ISpawner<T> _spawner;
        private readonly ICenterProvider _spawnOrigin;
        private int _bigAsteroidCount;

        public AsteroidController(
            ISpawner<T> spawner, 
            ICenterProvider spawnOrigin, 
            int bigAsteroidCount)
        {
            if (bigAsteroidCount < 1) throw new ArgumentOutOfRangeException("bigAsteroidCount");

            _spawner = spawner;
            _spawnOrigin = spawnOrigin;
            _bigAsteroidCount = bigAsteroidCount;
            AsteroidsLeft = GetAsteroidsToDestroy();

            _spawner.Destroyed += OnAsteroidDestroyed;
            _spawner.Spawn(_bigAsteroidCount, _spawnOrigin.Center);
        }

        public int AsteroidsLeft { get; private set; }

        private void OnAsteroidDestroyed(T obj)
        {
            if (--AsteroidsLeft == 0)
            {
                _spawner.Spawn(++_bigAsteroidCount, _spawnOrigin.Center);
                AsteroidsLeft = GetAsteroidsToDestroy();
            }
        }

        private int GetAsteroidsToDestroy()
        {
            return _bigAsteroidCount * 7;
        }
    }
}
