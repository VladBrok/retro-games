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

        public void Initialize(
            WraparoundBase<PlayerShip> wraparound, 
            Bounds viewArea, 
            Transform projectileContainer)
        {
            base.Initialize(wraparound);
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();

            var input = new KeyboardInput();
            _engine = new ShipEngine(input, _rigidbody);

            var weapon = GetComponent<ShipWeapon>();
            weapon.Initialize(
                viewArea, 
                input, 
                projectileContainer,
                p => new Wraparound<Projectile>(p, viewArea),
                () => transform.up);
            weapon.Fired += () => _animator.SetTrigger("shot");
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
