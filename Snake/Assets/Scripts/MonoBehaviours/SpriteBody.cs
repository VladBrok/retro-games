using UnityEngine;

namespace SnakeGame.MonoBehaviours
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteBody : MonoBehaviour, IBody
    {
        private Vector2 _size;

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
        public Vector2 Size { get { return _size; } }

        protected virtual void Awake()
        {
            var renderer = GetComponent<SpriteRenderer>();
            _size = new Vector2(
                renderer.size.x * transform.localScale.x,
                renderer.size.y * transform.localScale.y);
        }
    }
}
