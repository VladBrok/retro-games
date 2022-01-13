using System;
using UnityEngine;

namespace SnakeGame.MonoBehaviours
{
    [RequireComponent(typeof(Collider2D))]
    public class TriggerBody : MonoBehaviour, IBody, ITrigger
    {
        public event Action TriggerEntered = delegate { };

        public Vector2 Position
        {
            get { return transform.position; }
            set { transform.position = value; }
        }
        public Vector2 Size 
        { 
            get { return Vector2.one; } 
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEntered();
        }

        protected virtual void Awake()
        {
            Debug.Assert(
                GetComponent<Collider2D>().isTrigger,
                "Collider2D of the " + gameObject.name + " should be a trigger.");
        }
    }
}
