using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SnakeGame
{
    public class Snake : ISnake
    {
        public static readonly int MinBodyCount = 2;

        private LinkedList<IBody> _bodies;
        private Vector2 _movementDirection;

        public Snake(IBody[] bodies, Vector2 movementDirection)
        {
            if (bodies.Count() < MinBodyCount)
            {
                throw new ArgumentException("Minimum body count: " + MinBodyCount);
            }

            _bodies = new LinkedList<IBody>(bodies);
            ChangeMovementDirection(movementDirection);
        }

        public Vector2 MovementDirection 
        { 
            get { return _movementDirection; } 
        }
        public IBody Head
        {
            get { return _bodies.First.Value; }
        }
        public IBody Tip
        {
            get { return _bodies.Last.Value; }
        }

        public void Move()
        {
            IBody head = _bodies.First.Value;
            IBody tip = _bodies.Last.Value;
            _bodies.RemoveFirst();
            _bodies.RemoveLast();

            tip.Position = head.Position;
            head.Position += new Vector2(
                head.Size.x * _movementDirection.x,
                head.Size.y * _movementDirection.y);

            _bodies.AddFirst(tip);
            _bodies.AddFirst(head);
        }

        public void AddBody(IBody item)
        {
            item.Position = _bodies.Last.Value.Position;
            _bodies.AddLast(item);
        }

        public void ChangeMovementDirection(Vector2 newDirection)
        {
            if ((newDirection.x != 0f && newDirection.y != 0f)
                 || newDirection == Vector2.zero)
            {
                throw new ArgumentException(newDirection.ToString());
            }

            if (_movementDirection.x != 0f && newDirection.x == 0f
                || _movementDirection.y != 0f && newDirection.y == 0f
                || _movementDirection == default(Vector2))
            {
                _movementDirection = new Vector2(
                    Math.Sign(newDirection.x), 
                    Math.Sign(newDirection.y));
            }
        }
    }
}
