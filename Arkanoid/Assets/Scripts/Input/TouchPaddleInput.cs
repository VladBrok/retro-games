using UnityEngine;

namespace Arkanoid.Input
{
    public class TouchPaddleInput : IPaddleInput
    {
        private readonly Camera _camera;
        private readonly Transform _paddle;

        public TouchPaddleInput(Camera camera, Transform paddle)
        {
            _camera = camera;
            _paddle = paddle;
        }

        public Vector2 MovementDirection
        {
            get
            {
                if (UnityEngine.Input.touchCount == 0) return Vector2.zero;

                Vector2 touchPosition = GetWorldPositionOfTouch();
                return touchPosition.x < _paddle.position.x 
                       ? Vector2.left 
                       : Vector2.right;
            }
        }
        public bool LaunchBall 
        {
            get 
            {
                var paddleBounds = new Bounds(_paddle.position, Vector3.one);
                return paddleBounds.Contains(GetWorldPositionOfTouch());
            }
        }

        private Vector2 GetWorldPositionOfTouch()
        {
            Touch touch = UnityEngine.Input.GetTouch(0);
            return _camera.ScreenToWorldPoint(touch.position);
        }
    }
}
