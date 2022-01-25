using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    public class PlayerShip : Destructible<PlayerShip>
    {
        [SerializeField] private ProjectileConfig _projectileConfig;
        [SerializeField] private ShipConfig _shipConfig;
        [SerializeField] private Transform _firePoint;

        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private Bounds _viewArea;
        private ShipEngine _engine;
        private Weapon<Projectile> _weapon;

        public void Initialize(
            WraparoundBase<PlayerShip> wraparound, 
            Bounds viewArea, 
            Transform projectileContainer)
        {
            base.Initialize(wraparound);
            _viewArea = viewArea;
            _animator = GetComponent<Animator>();

            // FIXME: Pass a keyboard input as a dependency
            var input = new KeyboardInput();

            _engine = new ShipEngine(input, _rigidbody);

            var projectilePool = new Pool<Projectile>(
                _projectileConfig.Prefab,
                _firePoint,
                projectileContainer,
                InitializeProjectile);

            _weapon = new Weapon<Projectile>(
                input, projectilePool, _projectileConfig.FireRateInSeconds);
            _weapon.Fired += () => _animator.SetTrigger("shot");
        }

        public override void Destroy()
        {
            _animator.SetTrigger("destroy");
            _engine.Stop();
            base.Destroy();
        }

        protected override void Awake()
        {
            base.Awake();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        protected override void Update()
        {
            base.Update();
            _weapon.Update(Time.deltaTime);
            _engine.Update();
            _animator.SetBool("accelerating", _engine.Accelerating);
        }

        private void FixedUpdate()
        {
            _engine.FixedUpdate(_shipConfig.AccelerationForce, _shipConfig.TurnAngle);
        }

        private void InitializeProjectile(Projectile obj)
        {
            obj.Initialize(
                new Wraparound<Projectile>(obj, _viewArea),
                new ConsistentMovement(obj, _projectileConfig.Speed, transform.up),
                () => transform.up,
                _projectileConfig.LifetimeInSeconds);
        }
    }
}
