using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Config/Ship")]
    public class ShipConfig : ScriptableObject
    {
        [SerializeField] private float _accelerationForce;
        [SerializeField] private float _turnAngle;

        public float AccelerationForce
        {
            get { return _accelerationForce; }
        }
        public float TurnAngle
        {
            get { return _turnAngle; }
        }
    }
}
