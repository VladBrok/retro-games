using System;
using UnityEngine;

namespace Asteroids
{
    public class ShipWeapon : MonoBehaviour
    {
        [SerializeField] private ProjectileConfig _projectileConfig;
        [SerializeField] private Transform _firePoint;

        private Weapon<Projectile> _weapon;

        public event Action Fired = delegate { };

        public void Initialize(
            IWeaponInput input, 
            Transform projectileContainer,
            Func<Projectile, WraparoundBase<Projectile>> getProjectileWraparound,
            Func<Vector2> getProjectileDirection,
            bool fireImmediately = false)
        {
            var projectilePool = new Pool<Projectile>(
                _projectileConfig.Prefab,
                _firePoint,
                projectileContainer,
                p => InitializeProjectile(p, getProjectileWraparound, getProjectileDirection));

            _weapon = new Weapon<Projectile>(
               input, 
               projectilePool, 
               _projectileConfig.FireRateInSeconds,
               fireImmediately);
            _weapon.Fired += () => Fired();
        }

        private void InitializeProjectile(
            Projectile obj, 
            Func<Projectile, WraparoundBase<Projectile>> getWraparound,
            Func<Vector2> getDirection)
        {
            obj.Initialize(
                getWraparound(obj),
                new ConsistentMovement(obj, _projectileConfig.Speed, transform.up),
                getDirection,
                _projectileConfig.LifetimeInSeconds);
        }

        private void Update()
        {
            _weapon.Update(Time.deltaTime);
        }
    }
}
