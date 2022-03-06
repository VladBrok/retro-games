using System;
using UnityEngine;

namespace Arkanoid.Pickups
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PickupBase : MonoBehaviour
    {
        public event Action<PickupBase> TriggerEntered = delegate { };

        public virtual void Initialize(PickupConfig config)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.down * config.FallForce);
        }

        protected virtual void Awake()
        {
            Debug.Assert(
                GetComponent<Collider2D>().isTrigger, 
                gameObject.name + " should have a trigger collider.");
        }

        protected abstract void ApplyEffect();

        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEntered.Invoke(this);
            if (!other.GetComponent<Paddle>()) return;

            ApplyEffect();
        }
    }
}