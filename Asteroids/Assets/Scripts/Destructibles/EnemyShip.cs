using UnityEngine;
using Asteroids.Extensions;

namespace Asteroids
{
    [RequireComponent(typeof(ShipWeapon))]
    public class EnemyShip : ConsistentlyMovingObject<EnemyShip>
    {
        public void Initialize(
            WraparoundBase<EnemyShip> wraparound, 
            IMovement movement, 
            Transform projectileContainer,
            Bounds viewArea)
        {
            base.Initialize(wraparound, movement);

            var input = new AiInput();
            var weapon = GetComponent<ShipWeapon>();
            weapon.Initialize(
                viewArea, 
                input, 
                projectileContainer,
                p => new NoWraparound<Projectile>(p, viewArea),
                () => Vector2.one.RandomDirection());
        }
    }
}
