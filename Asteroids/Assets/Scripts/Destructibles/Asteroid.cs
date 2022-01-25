using System;
using UnityEngine;
using Asteroids.Extensions;
using Random = UnityEngine.Random;
using System.Collections;

namespace Asteroids
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Asteroid : ConsistentlyMovingObject<Asteroid>
    {
        private AsteroidConfig _config;

        public AsteroidConfig Config
        {
            get { return _config; }
        }

        public void Initialize(
            Wraparound<Asteroid> wraparound, 
            IMovement movement, 
            AsteroidConfig config)
        {
            base.Initialize(wraparound, movement);
            _config = config;
        }

        public override void Show()
        {
            base.Show();
            RandomizeOffset();
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
