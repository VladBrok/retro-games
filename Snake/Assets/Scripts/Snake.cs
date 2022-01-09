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
        public static readonly ReadOnlyCollection<Vector2> AllowedDirections = 
            Array.AsReadOnly(new Vector2[]
            {
                Vector2.up, 
                Vector2.down, 
                Vector2.left, 
                Vector2.right
            });

        private LinkedList<Body> _bodies;
        private Vector2 _movementDirection;

        public Snake(Body[] bodies, Vector2 movementDirection)
        {
            _bodies = new LinkedList<Body>(bodies);
            ChangeMovementDirection(movementDirection);
        }

        public void Move()
        {
            Body head = _bodies.First.Value;
            Body tip = _bodies.Last.Value;

            tip.Position = head.Position + new Vector2(
                head.Size.x * _movementDirection.x,
                head.Size.y * _movementDirection.y);

            _bodies.RemoveLast();
            _bodies.AddFirst(tip);
        }

        public void AddBody(Body item)
        {
            _bodies.AddLast(item);
        }

        public void ChangeMovementDirection(Vector2 value)
        {
            if (!AllowedDirections.Contains(value)) 
                throw new ArgumentException(value.ToString());

            if (_movementDirection.x == 0f && value.x != 0f
                || _movementDirection.y == 0f && value.y != 0f)
            {
                _movementDirection = value;
            }
        }
    }
}
