using System.Collections;
using UnityEngine;

namespace Arkanoid.Pickups.Effects
{
    public class UnstoppableBallEffect : PausableEffect
    {
        [SerializeField] [Range(1f, 100f)] private float _durationInSeconds;
        [SerializeField] [Range(0.3f, 1f)] private float _raycastDistance;
        [SerializeField] private LayerMask _brickLayer;
        [SerializeField] private Ball _ball;
        [SerializeField] private ParticleSystem _effect;

        private WaitWhile _waitWhilePaused;
        private float _duration;
        private bool _applied;

        public override void Apply()
        {
            _duration += _durationInSeconds;
            if (!_applied)
            {
                StartCoroutine(BecomeUnstoppableTemporarly());
            }
        }
    
        private IEnumerator BecomeUnstoppableTemporarly()
        {
            ToggleEffectActive(true);
            yield return ExecuteWhileNotPausedRoutine(_duration, () => _ball.Bounceable = !BrickIsAhead());
            _ball.Bounceable = true;
            ToggleEffectActive(false);
        }

        private void ToggleEffectActive(bool active)
        {
            _applied = active;
            _effect.gameObject.SetActive(active);
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
