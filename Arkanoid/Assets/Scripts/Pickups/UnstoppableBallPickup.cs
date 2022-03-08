using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arkanoid.Pickups
{
    public class UnstoppableBallPickup : PickupBase
    {
        [SerializeField] [Range(1f, 100f)] private float _durationInSeconds;
        [SerializeField] [Range(0.3f, 1f)] private float _raycastDistance;
        [SerializeField] private LayerMask _brickLayer;

        private Ball _ball;
        private ParticleSystem _effect;
        private static float s_duration;
        private static bool s_effectIsActive;

        public override void Initialize(PickupConfig config)
        {
            base.Initialize(config);
            _ball = config.Ball;
            _effect = config.UnstoppableBallEffect;
        }

        protected override void ApplyEffect()
        {
            Debug.Log(s_effectIsActive);
            s_duration += _durationInSeconds;
            if (!s_effectIsActive)
            {
                _ball.StartCoroutine(BecomeUnstoppableTemporarly());
            }
        }

        private IEnumerator BecomeUnstoppableTemporarly()
        {
            ToggleEffectActive(true);
            while (s_duration > 0f)
            {
                _ball.Bounceable = !BrickIsAhead();
                s_duration = Mathf.Max(0f, s_duration - Time.deltaTime);
                yield return null;
            }
            _ball.Bounceable = true;
            ToggleEffectActive(false);
        }

        private void ToggleEffectActive(bool active)
        {
            s_effectIsActive = active;
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
