using System;
using System.Collections;
using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Projectile : ConsistentlyMovingObject<Projectile>
    {
        private Func<Vector2> _getDirection;
        private float _lifetimeInSeconds;
        private float _lifetimeLeft;

        public void Initialize(
            WraparoundBase<Projectile> wraparound, 
            IMovement movement, 
            Func<Vector2> getDirection,
            float lifetimeInSeconds)
        {
            base.Initialize(wraparound, movement);
            _getDirection = getDirection;
            _lifetimeInSeconds = lifetimeInSeconds;
            _lifetimeLeft = _lifetimeInSeconds;
        }

        public override void Activate()
        {
            base.Activate();
            _lifetimeLeft = _lifetimeInSeconds;
            Movement.Direction = _getDirection();
        }

        protected override void Update()
        {
            base.Update();
            UpdateLifetime();
        }

        private void UpdateLifetime()
        {
            _lifetimeLeft -= Time.deltaTime;
            if (_lifetimeLeft <= 0f)
            {
                Destroy();
            }
        }
    }
}
