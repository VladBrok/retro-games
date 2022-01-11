using System;
using UnityEngine;

namespace SnakeGame.MonoBehaviours
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Food : SpriteBody, ITrigger
    {
        public event Action TriggerEntered = delegate { };

        public void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEntered();
        }

        protected override void Awake()
        {
            base.Awake();
            Debug.Assert(
                GetComponent<BoxCollider2D>().isTrigger,
                "Collider2D should be a trigger");
        }
    }
}
