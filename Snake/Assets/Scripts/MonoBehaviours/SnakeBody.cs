using UnityEngine;

namespace SnakeGame.MonoBehaviours
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SnakeBody : MonoBehaviour, IPositionProvider
    {
        private SpriteRenderer _renderer;

        public Vector2 Position
        {
            get
            {
                return transform.position;
            }
            set
            {
                transform.position = value;
            }
        }
        public SpriteRenderer Renderer
        {
            get
            {
                return _renderer;
            }
        }

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }
    }
}
