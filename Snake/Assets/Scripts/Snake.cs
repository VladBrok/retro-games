using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using UnityEngine;

namespace SnakeGame
{
    public class Snake
    {
        private LinkedList<IBody> _bodies;
        private Vector2 _movementDirection;

        public Snake(IBody[] bodies)
        {
            _bodies = new LinkedList<IBody>(bodies);
            _movementDirection = Vector2.up;
        }

        public Vector2 MovementDirection { get { return _movementDirection; } }

        public void Move()
        {
            IBody head = _bodies.First.Value;
            IBody tip = _bodies.Last.Value;
            _bodies.RemoveFirst();
            _bodies.RemoveLast();

            tip.Position = head.Position;
            head.Position = head.Position + new Vector2(
                head.Size.x * _movementDirection.x,
                head.Size.y * _movementDirection.y);

            _bodies.AddFirst(tip);
            _bodies.AddFirst(head);
        }

        public void AddBody(IBody item)
        {
            _bodies.AddLast(item);
        }

        public void ChangeMovementDirection(Vector2 newDirection)
        {
            if (newDirection.x != 0f && newDirection.y != 0f)
            {
                throw new ArgumentException(
                    "Either x or y component of the direction should be zero.", 
                    newDirection.ToString());
            }
            if (newDirection == Vector2.zero)
            {
                throw new ArgumentException(
                    "The direction should not be zero.",
                    newDirection.ToString());
            }

            if (_movementDirection.x != 0f && newDirection.x == 0f
                || _movementDirection.y != 0f && newDirection.y == 0f)
            {
                _movementDirection = newDirection;
            }
        }
    }
}
