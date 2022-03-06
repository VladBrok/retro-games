using UnityEngine;

namespace Arkanoid
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Paddle : MonoBehaviour
    {
        [SerializeField] private Ball _ball;
        [SerializeField] [Range(1f, 10f)] private float _speed;

        private Rigidbody2D _body;
        private Vector2 _startPosition;
        private Vector2 _direction;
        private bool _ballLaunched;

        public void Reset()
        {
            _ballLaunched = false;
            transform.position = _startPosition;
        }

        public void Pause()
        {
            enabled = false;
        }

        public void Unpause()
        {
            enabled = true;
        }

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _startPosition = transform.position;
        }

        private void Update()
        {
            _direction = GetDirectionFromInput();
            if (ShouldLaunchBall())
            {
                LaunchBall();
            }
        }

        private void FixedUpdate()
        {
            _body.MovePosition(_body.position + _direction * _speed * Time.fixedDeltaTime);
        }

        private Vector2 GetDirectionFromInput()
        {
            return Input.GetKey(KeyCode.A) ? Vector2.left :
                   Input.GetKey(KeyCode.D) ? Vector2.right :
                   Vector2.zero;
        }

        private bool ShouldLaunchBall()
        {
            return Input.GetKeyDown(KeyCode.Space) && !_ballLaunched;
        }

        private void LaunchBall()
        {
            _ballLaunched = true;
            _ball.Launch();
        }
    }
}