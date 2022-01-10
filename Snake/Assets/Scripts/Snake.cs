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

        public Snake(IBody[] bodies, Vector2 movementDirection)
        {
            _bodies = new LinkedList<IBody>(bodies);
            ChangeMovementDirection(movementDirection);
        }

        public void Move()
        {
            IBody head = _bodies.First.Value;
            IBody tip = _bodies.Last.Value;

            tip.Position = head.Position + new Vector2(
                head.Size.x * _movementDirection.x,
                head.Size.y * _movementDirection.y);

            _bodies.RemoveLast();
            _bodies.AddFirst(tip);
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
                    "Unable to move in two directions simultaneously", 
                    newDirection.ToString());
            }

            if (_movementDirection.x == 0f && newDirection.x != 0f
                || _movementDirection.y == 0f && newDirection.y != 0f)
            {
                _movementDirection = newDirection;
            }
        }
    }
}
