using UnityEngine;

namespace Arkanoid.Pickups
{
    public sealed class PickupConfig : MonoBehaviour
    {
        [SerializeField] private Paddle _paddle;
        [SerializeField] private UI _ui;
        [SerializeField] [Range(10f, 300f)] private float _fallForce;

        public Paddle Paddle { get { return _paddle; } }
        public UI UI { get { return _ui; } }
        public float FallForce { get { return _fallForce; } }
    }
}
