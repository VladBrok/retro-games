using System;
using UnityEngine;

namespace Arkanoid.Pickups
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class PickupBase : MonoBehaviour
    {
        public event Action<PickupBase> TriggerEntered = delegate { };

        private Rigidbody2D _body;
        private float _fallForce;

        public virtual void Initialize(PickupConfig config)
        {
            _fallForce = config.FallForce;
            StartFalling();
        }

        public void Pause()
        {
            _body.isKinematic = true;
            _body.velocity = Vector2.zero;
        }

        public void Unpause()
        {
            _body.isKinematic = false;
            StartFalling();
        }

        protected virtual void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
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

        private void StartFalling()
        {
            _body.AddForce(Vector2.down * _fallForce);            
        }
    }
}