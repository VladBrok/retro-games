using System;
using UnityEngine;

namespace Asteroids
{
    public class ConsistentMovement : IMovement
    {
        private readonly ICenterProvider _self;
        private readonly float _speed;
        private Vector2 _direction;

        public ConsistentMovement(ICenterProvider self, float speed, Vector2 direction)
        {
            _self = self;
            _speed = speed;
            _direction = direction;
        }

        public Vector2 Direction
        {
            set { _direction = value; }
        }

        public void Move(float deltaTime)
        {
            _self.Center += _direction * _speed * deltaTime;
        }
    }
}
