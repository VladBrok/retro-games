using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class EnemyController<T> where T : IDestructible
    {
        private readonly IPool<T> _pool;
        private readonly Bounds _viewArea;
        private readonly float _spawnOffset;

        public EnemyController(
            IPool<T> pool,
            ICoroutineStarter coroutineStarter,
            Bounds viewArea,
            Value spawnDelayInSeconds)
        {
            _pool = pool;
            _viewArea = viewArea;
            _spawnOffset = 1f;
            coroutineStarter.StartCoroutine(SpawnRoutine(spawnDelayInSeconds));
        }

        private IEnumerator SpawnRoutine(Value spawnDelay)
        {
            for (; ; )
            {
                yield return new WaitForSeconds(Random.Range(spawnDelay.Min, spawnDelay.Max));
                _pool.Get(GetSpawnOrigin());
            }
        }

        private Vector2 GetSpawnOrigin()
        {
            float randomValue = Random.Range(0f, 0.8f);

            return (randomValue <= 0.2f) ? GetPositionAtTop() :
                   (randomValue <= 0.4f) ? GetPositionAtRight() :
                   (randomValue <= 0.6f) ? GetPositionAtBottom() :
                   GetPositionAtLeft();
        }

        private Vector2 GetPositionAtTop()
        {
            return new Vector2(
                Random.Range(_viewArea.min.x, _viewArea.max.x),
                _viewArea.max.y + _spawnOffset);
        }

        private Vector2 GetPositionAtRight()
        {
            return new Vector2(
                _viewArea.max.x + _spawnOffset,
                Random.Range(_viewArea.min.y, _viewArea.max.y));
        }

        private Vector2 GetPositionAtBottom()
        {
            return new Vector2(
                Random.Range(_viewArea.min.x, _viewArea.max.x),
                _viewArea.min.y - _spawnOffset);
        }

        private Vector2 GetPositionAtLeft()
        {
            return new Vector2(
                _viewArea.min.x - _spawnOffset,
                Random.Range(_viewArea.min.y, _viewArea.max.y));
        }
    }
}
