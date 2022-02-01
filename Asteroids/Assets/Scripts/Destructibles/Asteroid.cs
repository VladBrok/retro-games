using System;
using System.Collections;
using UnityEngine;
using Asteroids.Extensions;
using Random = UnityEngine.Random;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SpriteRenderer))]
    public class Asteroid : ConsistentlyMovingObject<Asteroid>
    {
        private SpriteRenderer _renderer;
        private AsteroidConfig _config;

        public AsteroidConfig Config
        {
            get { return _config; }
        }

        public void Initialize(
            WraparoundBase<Asteroid> wraparound, 
            IMovement movement, 
            AsteroidConfig config)
        {
            base.Initialize(wraparound, movement);
            _renderer = GetComponent<SpriteRenderer>();
            _config = config;
        }

        public override void Activate()
        {
            base.Activate();
            RandomizeOffset();
            int randomIndex = Random.Range(0, _config.Sprites.Count);
            _renderer.sprite = _config.Sprites[randomIndex];
        }

        public void RandomizeOffset()
        {
            var randomOffset = new Vector3(
                RangeWithRandomSign(_config.SpawnOffsetX.Min, _config.SpawnOffsetX.Max),
                RangeWithRandomSign(_config.SpawnOffsetY.Min, _config.SpawnOffsetY.Max));
            transform.position += randomOffset;
        }

        private float RangeWithRandomSign(float min, float max)
        {
            return Random.Range(min, max).RandomizeSign();
        }
    }
}
