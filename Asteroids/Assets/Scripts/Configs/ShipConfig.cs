using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Config/Ship")]
    public class ShipConfig : ScriptableObject
    {
        [SerializeField, Range(0, 100)] private float _accelerationForce;
        [SerializeField, Range(0, 360)] private float _turnAngle;

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
