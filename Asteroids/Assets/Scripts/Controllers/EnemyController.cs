using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Asteroids.Extensions;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class EnemyController<T> where T : IDestructible
    {
        private readonly ISpawner<T> _spawner;
        private readonly Bounds _viewArea;

        public EnemyController(
            ISpawner<T> spawner,
            ICoroutineStarter coroutineStarter,
            Bounds viewArea,
            Value spawnDelayInSeconds)
        {
            _spawner = spawner;
            _viewArea = viewArea;
            coroutineStarter.StartCoroutine(SpawnRoutine(spawnDelayInSeconds));
        }

        private IEnumerator SpawnRoutine(Value spawnDelay)
        {
            for (; ; )
            {
                yield return new WaitForSeconds(Random.Range(spawnDelay.Min, spawnDelay.Max));
                _spawner.Spawn(1, GetSpawnOrigin());
            }
        }

        private Vector2 GetSpawnOrigin()
        {
            float spawnOffset = 1f;
            var spawnOrigin = Vector2.zero;
            float randomValue = Random.Range(0f, 0.8f);

            if (randomValue <= 0.2f)
            {
                spawnOrigin.x = Random.Range(_viewArea.min.x, _viewArea.max.x);
                spawnOrigin.y = _viewArea.max.y + spawnOffset;
            }
            else if (randomValue <= 0.4f)
            {
                spawnOrigin.x = _viewArea.max.x + spawnOffset;
                spawnOrigin.y = Random.Range(_viewArea.min.y, _viewArea.max.y);
            }
            else if (randomValue <= 0.6f)
            {
                spawnOrigin.x = Random.Range(_viewArea.min.x, _viewArea.max.x);
                spawnOrigin.y = _viewArea.min.y - spawnOffset;
            }
            else
            {
                spawnOrigin.x = _viewArea.min.x - spawnOffset;
                spawnOrigin.y = Random.Range(_viewArea.min.y, _viewArea.max.y);
            }

            return spawnOrigin;
        }
    }
}
