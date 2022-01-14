using System;
using UnityEngine;

namespace SnakeGame.Helpers
{
    public class Field
    {
        private readonly IBody _target;
        private readonly Bounds _area;

        public Field(IBody target, Bounds area)
        {
            _target = target;
            _area = area;
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
            return _area.Contains(targetPos);
        }
    }
}
