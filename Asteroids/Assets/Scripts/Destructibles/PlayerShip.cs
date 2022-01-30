using UnityEngine;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(ShipWeapon))]
    public class PlayerShip : Destructible<PlayerShip>
    {
        [SerializeField] private ShipConfig _shipConfig;

        private Rigidbody2D _rigidbody;
        private Animator _animator;
        private ShipEngine _engine;
        private Respawner _respawner;

        public void Initialize(
            WraparoundBase<PlayerShip> wraparound, 
            Bounds viewArea, 
            Transform projectileContainer,
            Respawner respawner)
        {
            base.Initialize(wraparound);
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();

            var input = new KeyboardInput();
            _engine = new ShipEngine(input, _rigidbody);

            _respawner = respawner;
            _respawner.Initialize(this, input, viewArea);

            var weapon = GetComponent<ShipWeapon>();
            weapon.Initialize(
                input, 
                projectileContainer,
                p => new Wraparound<Projectile>(p, viewArea),
                () => transform.up);
            weapon.Fired += () => _animator.SetTrigger("shot");
        }

        public void Respawn()
        {
            _respawner.RespawnAt(Vector2.zero);
        }

        public override void Destroy()
        {
            _animator.SetTrigger("destroy");
            _engine.Stop();
            base.Destroy();
        }

        protected override void Update()
        {
            base.Update();
            _engine.Update();
            _animator.SetBool("accelerating", _engine.Accelerating);
        }

        private void FixedUpdate()
        {
            _engine.FixedUpdate(_shipConfig.AccelerationForce, _shipConfig.TurnAngle);
        }
    }
}
