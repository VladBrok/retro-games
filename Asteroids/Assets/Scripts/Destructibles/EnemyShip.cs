using UnityEngine;
using Asteroids.Extensions;

// FIXME: The class contains duplicate code with the PlayerShip class.

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyShip : ConsistentlyMovingObject<EnemyShip>
    {
        [SerializeField] private ProjectileConfig _projectileConfig;
        [SerializeField] private Transform _firePoint;

        private Bounds _viewArea;
        private Weapon<Projectile> _weapon;

        public void Initialize(
            WraparoundBase<EnemyShip> wraparound, 
            IMovement movement, 
            Transform projectileContainer,
            Bounds viewArea)
        {
            base.Initialize(wraparound, movement);
            _viewArea = viewArea;

            var projectilePool = new Pool<Projectile>(
                _projectileConfig.Prefab,
                _firePoint,
                projectileContainer,
                InitializeProjectile);

            var input = new AiInput();
            _weapon = new Weapon<Projectile>(
                input, projectilePool, _projectileConfig.FireRateInSeconds);
        }

        protected override void Update()
        {
            base.Update();
            _weapon.Update(Time.deltaTime);
        }

        private void InitializeProjectile(Projectile obj)
        {
            obj.Initialize(
                new NoWraparound<Projectile>(obj, _viewArea),
                new ConsistentMovement(obj, _projectileConfig.Speed, transform.up),
                () => Vector2.one.RandomDirection(),
                _projectileConfig.LifetimeInSeconds);
        }
    }
}
