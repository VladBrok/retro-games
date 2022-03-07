using System.Collections;
using UnityEngine;

namespace Arkanoid.Pickups
{
    public class UnstoppableBallPickup : PickupBase
    {
        [SerializeField] [Range(1f, 100f)] private float _durationInSeconds;
        [SerializeField] [Range(0.3f, 1f)] private float _raycastDistance;
        [SerializeField] private LayerMask _brickLayer;

        private Ball _ball;

        public override void Initialize(PickupConfig config)
        {
            base.Initialize(config);
            _ball = config.Ball;
        }

        protected override void ApplyEffect()
        {
            _ball.StartCoroutine(BecomeUnstoppableTemporarly());
        }

        private IEnumerator BecomeUnstoppableTemporarly()
        {
            float effectDuration = _durationInSeconds;
            while (effectDuration > 0f)
            {
                _ball.Bounceable = !BrickIsAhead();
                effectDuration -= Time.deltaTime;
                yield return null;
            }
            _ball.Bounceable = true;
        }

        private bool BrickIsAhead()
        {
            return Physics2D.CircleCast(
                _ball.Position,
                _ball.Radius,
                _ball.MovementDirection,
                _raycastDistance,
                _brickLayer);
        }
    }
}
