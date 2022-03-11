using System;
using UnityEngine;
using Arkanoid.Pickups.Effects;

namespace Arkanoid.Pickups
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Pickup : MonoBehaviour, IPausable
    {
        public event Action<Pickup> TriggerEntered = delegate { };

        private Rigidbody2D _body;
        private SpriteRenderer _renderer;
        private EffectBase _effect;
        private float _fallForce;

        public void Initialize(EffectBase effect, float fallForce)
        {
            _renderer.sprite = effect.Sprite;
            _fallForce = fallForce;
            _effect = effect;
            StartFalling();
        }

        public void StartFalling()
        {
            _body.AddForce(Vector2.down * _fallForce);
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

        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
            _renderer = GetComponent<SpriteRenderer>();
            Debug.Assert(
                GetComponent<Collider2D>().isTrigger, 
                gameObject.name + " should have a trigger collider.");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            TriggerEntered.Invoke(this);
            if (!other.GetComponent<Paddle>()) return;

            _effect.Apply();
        }
    }
}