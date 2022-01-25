using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Config/Projectile")]
    public class ProjectileConfig : ScriptableObject
    {
        [SerializeField] private Projectile _prefab;
        [SerializeField] private float _fireRateInSeconds;
        [SerializeField] private float _lifetimeInSeconds;
        [SerializeField] private float _speed;

        public Projectile Prefab
        {
            get { return _prefab; }
        }
        public float FireRateInSeconds
        {
            get { return _fireRateInSeconds; }
        }
        public float LifetimeInSeconds
        {
            get { return _lifetimeInSeconds; }
        }
        public float Speed
        {
            get { return _speed; }
        }
    }
}
