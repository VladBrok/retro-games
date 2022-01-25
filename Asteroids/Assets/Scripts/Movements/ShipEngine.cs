using UnityEngine;

namespace Asteroids
{
    public class ShipEngine
    {
        private readonly IShipInput _input;
        private readonly Rigidbody2D _rigidbody;
        private bool _accelerating;
        private float _turnDirection;

        public ShipEngine(IShipInput input, Rigidbody2D rigidbody)
        {
            _input = input;
            _rigidbody = rigidbody;
        }

        public bool Accelerating
        {
            get { return _input.Accelerating; }
        }

        public void Update()
        {
            _accelerating = _input.Accelerating;
            _turnDirection = _input.TurnDirection;
        }

        public void FixedUpdate(float accelerationForce, float turnAngle)
        {
            if (_accelerating)
            {
                _rigidbody.AddRelativeForce(Vector2.up * accelerationForce);
            }

            if (_turnDirection != 0f)
            {
                _rigidbody.AddTorque(_turnDirection * turnAngle);
                _rigidbody.rotation += _turnDirection * turnAngle;
            }
            else
            {
                _rigidbody.angularVelocity = 0f;
            }
        }

        public void Stop()
        {
            _rigidbody.velocity = Vector2.zero;
            _rigidbody.angularVelocity = 0f;
            _rigidbody.rotation = 0f;
        }
    }
}
