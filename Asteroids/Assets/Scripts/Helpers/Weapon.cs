using System;

namespace Asteroids
{
    public class Weapon<T> where T : Destructible<T>
    {
        private readonly IWeaponInput _input;
        private readonly Pool<T> _projectilePool;
        private readonly float _fireRate;
        private float _cooldown;

        public Weapon(IWeaponInput input, Pool<T> projectilePool, float fireRate)
        {
            _input = input;
            _projectilePool = projectilePool;
            _fireRate = fireRate;
        }

        public event Action Fired = delegate { };

        public void Update(float deltaTime)
        {
            _cooldown -= deltaTime;
            if (_input.Fire && _cooldown <= 0f)
            {
                Fire();
            }
        }

        private void Fire()
        {
            _projectilePool.Get();
            _cooldown = _fireRate;
            Fired();
        }
    }
}
