using UnityEngine;

namespace Arkanoid
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(CircleCollider2D))]
    public class Ball : MonoBehaviour
    {
        [SerializeField] [Range(100f, 1000f)] private float _launchForce;
        [SerializeField] [Range(0.1f, 1f)] private float _directionOffset;
        [SerializeField] [Range(0f, 10f)] private float _speed;

        private Rigidbody2D _body;
        private Vector2 _startPosition;
        private Vector2 _pausedVelocity;
        private CircleCollider2D _collider;

        public bool Bounceable 
        {
            set { _collider.isTrigger = !value; }
        }
        public Vector2 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }
        public Vector2 MovementDirection
        {
            get { return _body.velocity.normalized; }
        }
        public float Radius
        {
            get { return _collider.radius; }
        }

        public void Launch()
        {
            ToggleEnabled(true);
            _body.AddForce(Vector2.up * _launchForce);
        }

        public void Reset()
        {
            transform.position = _startPosition;
            ToggleEnabled(false);
        }

        public void Pause()
        {
            _pausedVelocity = _body.velocity;
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
            _collider = GetComponent<CircleCollider2D>();
            _startPosition = transform.position;
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

        private float AdjustIfSmall(float directionAxis)
        {
            if (Mathf.Abs(directionAxis) <= 0.001f)
            {
                directionAxis = _directionOffset * RandomSign();
            }
            return directionAxis;
        }

        private void ToggleEnabled(bool enabled)
        {
            _body.isKinematic = !enabled;
            _collider.enabled = enabled;
            this.enabled = enabled;
            if (!enabled)
            {
                _body.velocity = Vector2.zero;
            }
        }

        private float RandomSign()
        {
            return Random.Range(1, 11) % 2 == 0 ? -1f : +1f;
        }
    }
}