using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SnakeGame
{
    public class Respawner
    {
        private readonly IBody _target;
        private readonly Bounds _bounds;
        private Action _beforeRespawn;

        public Respawner(IBody target, Bounds bounds, Action beforeRespawn)
        {
            _target = target;
            _bounds = bounds;
            _beforeRespawn = beforeRespawn;
        }

        public void RespawnTarget()
        {
            _beforeRespawn();
            _target.Position = GetRandomPosition();
        }

        private Vector2 GetRandomPosition()
        {
            float offset = 1f;
            return new Vector2(
                Mathf.Floor(Random.Range(_bounds.min.x, _bounds.max.x - offset)),
                Mathf.Floor(Random.Range(_bounds.min.y + offset, _bounds.max.y)));
        }
    }
}
