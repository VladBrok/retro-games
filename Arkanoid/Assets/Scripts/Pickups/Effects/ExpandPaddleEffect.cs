using System.Collections;
using UnityEngine;

namespace Arkanoid.Pickups.Effects
{
    public class ExpandPaddleEffect : EffectBase, IPausable
    {
        [SerializeField] [Range(1f, 10f)] private float _widthMultiplier;
        [SerializeField] [Range(1f, 100f)] private float _durationInSeconds;
        [SerializeField] private Paddle _paddle;

        private WaitWhile _waitWhilePaused;
        private bool _paused;

        private Vector3 PaddleScale
        {
            get { return _paddle.transform.localScale; }
            set { _paddle.transform.localScale = value; }
        }

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
            StartCoroutine(ExpandTemporarly());
        }

        private void Awake()
        {
            _waitWhilePaused = new WaitWhile(() => _paused);
        }

        private IEnumerator ExpandTemporarly()
        {
            UpdatePaddleWidth(PaddleScale.x * _widthMultiplier);
            float duration = _durationInSeconds;
            while (duration > 0f)
            {
                if (_paused) yield return _waitWhilePaused;

                duration -= Time.deltaTime;
                yield return null;
            }
            UpdatePaddleWidth(PaddleScale.x / _widthMultiplier);
        }

        private void UpdatePaddleWidth(float @new)
        {
            PaddleScale = new Vector3(
                @new,
                PaddleScale.y,
                PaddleScale.z);
        }
    }
}
