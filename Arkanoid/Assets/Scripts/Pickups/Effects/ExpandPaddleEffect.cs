using System.Collections;
using UnityEngine;

namespace Arkanoid.Pickups.Effects
{
    public class ExpandPaddleEffect : PausableEffect
    {
        [SerializeField] [Range(1f, 10f)] private float _widthMultiplier;
        [SerializeField] [Range(1f, 100f)] private float _durationInSeconds;
        [SerializeField] private Paddle _paddle;

        private Vector3 PaddleScale
        {
            get { return _paddle.transform.localScale; }
            set { _paddle.transform.localScale = value; }
        }

        public override void Apply()
        {
            StartCoroutine(ExpandTemporarly());
        }

        private IEnumerator ExpandTemporarly()
        {
            UpdatePaddleWidth(PaddleScale.x * _widthMultiplier);
            yield return ExecuteWhileNotPausedRoutine(_durationInSeconds, delegate { });
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
