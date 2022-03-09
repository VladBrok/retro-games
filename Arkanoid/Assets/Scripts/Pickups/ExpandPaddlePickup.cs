using System.Collections;
using UnityEngine;

namespace Arkanoid.Pickups
{
    public class ExpandPaddlePickup : PickupBase
    {
        [SerializeField] [Range(1f, 10f)] private float _widthMultiplier;
        [SerializeField] [Range(1f, 100f)] private float _durationInSeconds;

        private WaitForSeconds _waitForEffectDuration;
        private Paddle _paddle;
        private MonoBehaviour _coroutineRunner;

        private Vector3 PaddleScale
        {
            get { return _paddle.transform.localScale; }
            set { _paddle.transform.localScale = value; }
        }

        public override void Initialize(PickupConfig config)
        {
            base.Initialize(config);
            _paddle = config.Paddle;
            _coroutineRunner = config.CoroutineRunner;
        }

        protected override void Awake()
        {
            base.Awake();
            _waitForEffectDuration = new WaitForSeconds(_durationInSeconds);
        }

        protected override void ApplyEffect()
        {
            _coroutineRunner.StartCoroutine(ExpandTemporarly());
        }

        private IEnumerator ExpandTemporarly()
        {
            UpdatePaddleWidth(PaddleScale.x * _widthMultiplier);
            yield return _waitForEffectDuration;
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
