using System;
using UnityEngine;

namespace SnakeGame
{
    public class Field
    {
        private readonly IBody _target;
        private readonly Bounds _bounds;

        public Field(IBody target, Bounds bounds)
        {
            _target = target;
            _bounds = bounds;
        }

        public event Action TargetLeftField = delegate { };

        public void Update()
        {
            if (!IsTargetOnField())
            {
                TargetLeftField();
            }
        }

        private bool IsTargetOnField()
        {
            float offset = 0.5f;
            var snakePos = new Vector2(
                _target.Position.x + offset,
                _target.Position.y - offset);
            return _bounds.Contains(snakePos);
        }
    }
}
