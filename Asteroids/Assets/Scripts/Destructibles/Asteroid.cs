using UnityEngine;
using Asteroids.Extensions;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Asteroid : ConsistentlyMovingObject<Asteroid>
    {
        private SpriteRenderer _renderer;
        private AsteroidConfig _config;
        private ISoundEffectsPlayer _sfxPlayer;

        public AsteroidConfig Config
        {
            get { return _config; }
        }

        public void Initialize(
            WraparoundBase<Asteroid> wraparound, 
            IMovement movement, 
            AsteroidConfig config,
            ISoundEffectsPlayer sfxPlayer)
        {
            base.Initialize(wraparound, movement);
            _config = config;
            _sfxPlayer = sfxPlayer;
        }

        public override void Activate()
        {
            base.Activate();
            RandomizeOffset();
            _renderer.sprite = _config.Sprites.TakeRandom();
        }

        public override void Destroy()
        {
            _sfxPlayer.PlayOneShot(SoundEffectType.Explosion);
            base.Destroy();
        }

        protected override void Awake()
        {
            base.Awake();
            _renderer = GetComponent<SpriteRenderer>();
        }

        private void RandomizeOffset()
        {
            var randomOffset = new Vector3(
                _config.SpawnOffsetX.Randomize().RandomizeSign(),
                _config.SpawnOffsetY.Randomize().RandomizeSign());
            transform.position += randomOffset;
        }
    }
}
