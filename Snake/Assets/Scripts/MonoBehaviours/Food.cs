using System;
using UnityEngine;

namespace SnakeGame.MonoBehaviours
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Food : MonoBehaviour, ITrigger
    {
        public event Action TriggerEntered = delegate { };

        private void Awake()
        {
            Debug.Assert(
                GetComponent<BoxCollider2D>().isTrigger, 
                "Collider2D should be a trigger");
        }

        public void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEntered();
        }
    }
}
