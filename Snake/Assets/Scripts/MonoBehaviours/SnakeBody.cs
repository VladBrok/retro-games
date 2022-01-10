using UnityEngine;

namespace SnakeGame.MonoBehaviours
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SnakeBody : MonoBehaviour, IBody
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
        public Vector2 Size
        {
            get
            {
                return _renderer.size;
            }
        }

        private void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
        }
    }
}
