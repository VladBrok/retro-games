using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arkanoid.Pickups.Effects
{
    public class UnstoppableBallEffect : EffectBase, IPausable
    {
        [SerializeField] [Range(1f, 100f)] private float _durationInSeconds;
        [SerializeField] [Range(0.3f, 1f)] private float _raycastDistance;
        [SerializeField] private LayerMask _brickLayer;
        [SerializeField] private Ball _ball;
        [SerializeField] private ParticleSystem _effect;

        private WaitWhile _waitWhilePaused;
        private float _duration;
        private bool _applied;
        private bool _paused;

        public void Pause()
        {
            _paused = true;
        }

        public void Unpause()
        {
            _paused = false;
        }

        public override void Apply()
        {
            _duration += _durationInSeconds;
            if (!_applied)
            {
                StartCoroutine(BecomeUnstoppableTemporarly());
            }
        }

        private void Awake()
        {
            _waitWhilePaused = new WaitWhile(() => _paused);
        }
    
        private IEnumerator BecomeUnstoppableTemporarly()
        {
            ToggleEffectActive(true);
            while (_duration > 0f)
            {
                if (_paused) yield return _waitWhilePaused;

                _ball.Bounceable = !BrickIsAhead();
                _duration = Mathf.Max(0f, _duration - Time.deltaTime);
                yield return null;
            }
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
