using System;
using System.Collections;
using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(SoundEffectsPlayer))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Projectile : ConsistentlyMovingObject<Projectile>
    {
        private SoundEffectsPlayer _sfxPlayer;
        private SpriteRenderer _renderer;
        private float _lifetimeInSeconds;
        private float _lifetimeLeft;
        private Func<Vector2> _getDirection;
        private WaitWhile _waitWhileSoundPlaying;
        private bool _destroying;

        public void Initialize(
            WraparoundBase<Projectile> wraparound, 
            IMovement movement, 
            Func<Vector2> getDirection,
            float lifetimeInSeconds)
        {
            base.Initialize(wraparound, movement);
            _getDirection = getDirection;
            _lifetimeInSeconds = lifetimeInSeconds;
        }

        public override void Deactivate()
        {
            base.Deactivate();
            _destroying = false;
            _lifetimeLeft = _lifetimeInSeconds;
            Movement.Direction = _getDirection();
        }

        public override void Destroy()
        {
            StartCoroutine(DestroyRoutine());
        }

        protected override void Awake()
        {
            base.Awake();
            _sfxPlayer = GetComponent<SoundEffectsPlayer>();
            _renderer = GetComponent<SpriteRenderer>();
            _waitWhileSoundPlaying = new WaitWhile(() => _sfxPlayer.IsPlaying);
            _lifetimeLeft = _lifetimeInSeconds;
        }

        protected override void Update()
        {
            if (_destroying) return;

            base.Update();
            UpdateLifetime();
        }

        private void UpdateLifetime()
        {
            _lifetimeLeft -= Time.deltaTime;
            if (_lifetimeLeft <= 0f)
            {
                base.Destroy();
            }
        }

        private IEnumerator DestroyRoutine()
        {
            _destroying = true;
            _sfxPlayer.PlayOneShot(SoundEffectType.Explosion);
            _renderer.enabled = false;
            GetComponent<Collider2D>().enabled = false;

            yield return _waitWhileSoundPlaying;

            GetComponent<Collider2D>().enabled = true;
            _renderer.enabled = true;
            base.Destroy();
        }
    }
}
