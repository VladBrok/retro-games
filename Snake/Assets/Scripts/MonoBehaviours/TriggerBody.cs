using System;
using UnityEngine;

namespace SnakeGame.MonoBehaviours
{
    [RequireComponent(typeof(Collider2D))]
    public class TriggerBody : MonoBehaviour, IBody, ITrigger
    {
        private Vector2 _size;

        public event Action TriggerEntered = delegate { };

        public Vector2 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }
        public Vector2 Size 
        { 
            get { return _size; } 
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEntered();
        }

        protected virtual void Awake()
        {
            var collider = GetComponent<Collider2D>();

            Debug.Assert(
                collider.isTrigger,
                "Collider2D should be a trigger");

            _size = collider.bounds.size;
        }
    }
}
