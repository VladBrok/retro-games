using System;
using UnityEngine;

// FIXME: The class is very similar to the AsteroidSpawner class.

namespace Asteroids
{
    public class EnemySpawner : ISpawner<EnemyShip>
    {
        private readonly EnemyShip _prefab;
        private readonly Bounds _viewArea;
        private readonly Camera _camera;
        private readonly Pool<EnemyShip> _pool;

        public EnemySpawner(
            EnemyShip prefab, 
            Transform enemyContainer,
            Transform projectileContainer,
            Bounds viewArea, 
            Camera camera)
        {
            _prefab = prefab;
            _viewArea = viewArea;
            _camera = camera;
            _pool = new Pool<EnemyShip>(
                _prefab, null, enemyContainer, enemy => Initialize(enemy, projectileContainer));
        }

        public event Action<EnemyShip> Destroyed = delegate { };

        void ISpawner<EnemyShip>.Spawn(int count, Vector2 origin)
        {
            for (int i = 0; i < count; i++)
            {
                CreateEnemy(origin);
            }
        }

        private void CreateEnemy(Vector2 origin)
        {
            EnemyShip enemy = _pool.Get();
            enemy.transform.position = origin;
            Vector2 movementDir = GetMovementDirection(origin);
            enemy.Movement.Direction = movementDir;
            enemy.transform.rotation = Quaternion.LookRotation(Vector3.forward, movementDir);
        }

        private Vector2 GetMovementDirection(Vector2 origin)
        {
            Vector2 viewportPos = _camera.WorldToViewportPoint(origin);
            return viewportPos.x <= 0f ? Vector2.right :
                   viewportPos.x >= 1f ? Vector2.left :
                   viewportPos.y <= 0f ? Vector2.up :
                   Vector2.down;
        }

        private void Initialize(EnemyShip enemy, Transform projectileContainer)
        {
            enemy.Initialize(
                new Wraparound<EnemyShip>(enemy, _viewArea), 
                new ConsistentMovement(enemy, 2f, Vector2.zero),
                projectileContainer,
                _viewArea);
            enemy.Destroyed += () => Destroyed(enemy);
        }
    }
}
