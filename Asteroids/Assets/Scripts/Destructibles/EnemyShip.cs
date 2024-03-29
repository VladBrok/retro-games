﻿using UnityEngine;
using Asteroids.Extensions;

namespace Asteroids
{
    [RequireComponent(typeof(ShipWeapon))]
    public class EnemyShip : ConsistentlyMovingObject<EnemyShip>
    {
        private Camera _camera;
        private ISoundEffectsPlayer _sfxPlayer;

        public void Initialize(
            WraparoundBase<EnemyShip> wraparound, 
            IMovement movement, 
            Transform projectileContainer,
            Bounds viewArea,
            Camera camera,
            ISoundEffectsPlayer sfxPlayer)
        {
            base.Initialize(wraparound, movement);

            _camera = camera;
            _sfxPlayer = sfxPlayer;

            var input = new AiInput();
            var weapon = GetComponent<ShipWeapon>();
            weapon.Initialize(
                input, 
                projectileContainer,
                p => new NoWraparound<Projectile>(p, viewArea),
                () => Vector2.one.RandomDirection(),
                fireImmediately: true);
            weapon.Fired += () => _sfxPlayer.PlayOneShot(SoundEffectType.LaserShot);
        }

        public override void Activate()
        {
            base.Activate();
            Vector2 movementDir = GetMovementDirection();
            Movement.Direction = movementDir;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, movementDir);
        }

        public override void Destroy()
        {
            _sfxPlayer.PlayOneShot(SoundEffectType.Explosion);
            base.Destroy();
        }

        private Vector2 GetMovementDirection()
        {
            Vector2 viewportPos = _camera.WorldToViewportPoint(transform.position);
            return (viewportPos.x <= 0f) ? Vector2.right :
                   (viewportPos.x >= 1f) ? Vector2.left :
                   (viewportPos.y <= 0f) ? Vector2.up :
                   Vector2.down;
        }
    }
}
