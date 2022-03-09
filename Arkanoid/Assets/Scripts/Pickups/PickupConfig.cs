using UnityEngine;

namespace Arkanoid.Pickups
{
    public sealed class PickupConfig : MonoBehaviour
    {
        [SerializeField] private Paddle _paddle;
        [SerializeField] private MainUI _ui;
        [SerializeField] private Ball _ball;
        [SerializeField] [Range(10f, 300f)] private float _fallForce;
        [SerializeField] private ParticleSystem _unstoppableBallEffect;
        [SerializeField] private MonoBehaviour _coroutineRunner;

        public Paddle Paddle { get { return _paddle; } }
        public MainUI UI { get { return _ui; } }
        public Ball Ball { get { return _ball; } }
        public float FallForce { get { return _fallForce; } }
        public ParticleSystem UnstoppableBallEffect { get { return _unstoppableBallEffect; } }
        public MonoBehaviour CoroutineRunner { get { return _coroutineRunner; } }
    }
}
