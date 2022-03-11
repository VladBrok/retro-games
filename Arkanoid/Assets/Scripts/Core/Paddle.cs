using UnityEngine;
using Arkanoid.Input;

namespace Arkanoid
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Paddle : MonoBehaviour, IPausable
    {
        [SerializeField] private Ball _ball;
        [SerializeField] [Range(1f, 10f)] private float _speed;

        private Rigidbody2D _body;
        private Vector2 _startPosition;
        private Vector2 _direction;
        private bool _ballLaunched;
        private IPaddleInput _input;

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
#if UNITY_IOS || UNITY_ANDROID
            _input = new TouchPaddleInput(Camera.main, transform);
#else
            _input = new KeyboardPaddleInput();
#endif
        }

        private void Update()
        {
            _direction = _input.MovementDirection;
            if (_ballLaunched) return;
            
            PutBallOnPaddle();
            if (_input.LaunchBall)
            {
                LaunchBall();
            }
        }

        private void FixedUpdate()
        {
            _body.MovePosition(_body.position + _direction * _speed * Time.fixedDeltaTime);
        }

        private void LaunchBall()
        {
            _ballLaunched = true;
            _ball.Launch();
        }

        private void PutBallOnPaddle()
        {
            _ball.Position = new Vector2(
                _body.position.x,
                _ball.Position.y);
        }
    }
}