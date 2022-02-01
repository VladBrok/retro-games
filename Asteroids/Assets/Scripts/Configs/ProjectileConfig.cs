using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Config/Projectile")]
    public class ProjectileConfig : ScriptableObject
    {
        [SerializeField] private Projectile _prefab;
        [SerializeField, Range(0, 100)] private float _fireRateInSeconds;
        [SerializeField, Range(0, 100)] private float _lifetimeInSeconds;
        [SerializeField, Range(0, 100)] private float _speed;

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
