using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SnakeGame
{
    public class Snake<T> where T : IBody
    {
        private LinkedList<T> _bodies;
        private Vector2 _movementDirection;
        private T _bodyToAdd;

        public Snake(T[] bodies)
        {
            if (bodies.Count() < 2)
            {
                throw new ArgumentException("Minimum body count: 2");
            }

            _bodies = new LinkedList<T>(bodies);
            _movementDirection = Vector2.up;
        }

        public event Action<T> BodyAdded = delegate { };

        public Vector2 MovementDirection { get { return _movementDirection; } }

        public void Move()
        {
            T head = _bodies.First.Value;
            T tip = _bodies.Last.Value;
            _bodies.RemoveFirst();
            _bodies.RemoveLast();

            AddBodyIfNeeded(tip); 

            tip.Position = head.Position;
            head.Position = head.Position + new Vector2(
                head.Size.x * _movementDirection.x,
                head.Size.y * _movementDirection.y);

            _bodies.AddFirst(tip);
            _bodies.AddFirst(head);
        }

        public void RequestBodyAddition(T item)
        {
            _bodyToAdd = item;
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

        private void AddBodyIfNeeded(T tip)
        {
            if (_bodyToAdd != null)
            {
                _bodyToAdd.Position = tip.Position;
                _bodies.AddLast(_bodyToAdd);
                _bodyToAdd = default(T);
                BodyAdded(_bodies.Last.Value);
            }
        }
    }
}
