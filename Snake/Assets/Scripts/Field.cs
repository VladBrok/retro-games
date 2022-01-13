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
            Vector2 offset = _target.Size / 2;
            var targetPos = new Vector2(
                _target.Position.x + offset.x,
                _target.Position.y - offset.y);
            return _bounds.Contains(targetPos);
        }
    }
}
