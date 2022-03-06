using UnityEngine;

namespace Arkanoid
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] [Range(100f, 1000f)] private float _launchForce;
        [SerializeField] [Range(0f, 1f)] private float _directionOffset;
        [SerializeField] [Range(0f, 10f)] private float _speed;

        private Rigidbody2D _body;
        private Vector2 _startPosition;
        private Transform _parent;
        private Vector2 _pausedVelocity;

        public void Launch()
        {
            ToggleEnabled(true);
            transform.parent = null;
            _body.AddForce(Vector2.up * _launchForce);
        }

        public void Reset()
        {
            transform.parent = _parent;
            transform.position = _startPosition;
            _body.velocity = Vector2.zero;
            ToggleEnabled(false);
        }

        public void Pause()
        {
            _pausedVelocity = _body.velocity;
            _body.velocity = Vector2.zero;
            ToggleEnabled(false);
        }

        public void Unpause()
        {
            if (_pausedVelocity == Vector2.zero) return;

            ToggleEnabled(true);
            _body.velocity = _pausedVelocity;
        }

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _startPosition = transform.position;
            _parent = transform.parent;
            ToggleEnabled(false);
        }

        private void OnCollisionEnter2D(Collision2D _)
        {
            AdjustVelocity();
        }

        private void OnCollisionExit2D(Collision2D _)
        {
            AdjustVelocity();
        }

        private void FixedUpdate()
        {
            AdjustVelocity();
        }

        private void AdjustVelocity()
        {
            Vector2 direction = _body.velocity.normalized;
            direction = new Vector2(
                AdjustIfSmall(direction.x),
                AdjustIfSmall(direction.y));
            _body.velocity = direction * _speed;
        }

        private float AdjustIfSmall(float value)
        {
            return Mathf.Abs(value) <= 0.001f ? _directionOffset * RandomSign() : value;
        }

        private void ToggleEnabled(bool enabled)
        {
            _body.isKinematic = !enabled;
            this.enabled = enabled;
        }

        private float RandomSign()
        {
            return Random.Range(1, 11) % 2 == 0 ? -1f : +1f;
        }
    }
}