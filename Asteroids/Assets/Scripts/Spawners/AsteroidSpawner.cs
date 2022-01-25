using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Asteroids.Extensions;
using Random = UnityEngine.Random;

namespace Asteroids
{
    public class AsteroidSpawner : ISpawner<Asteroid>
    {
        private readonly Dictionary<AsteroidType, AsteroidConfig> _configs;
        private readonly Bounds _viewArea;
        private Dictionary<AsteroidType, Pool<Asteroid>> _pools;

        public AsteroidSpawner(
            AsteroidConfig[] configs,
            Transform container,
            Bounds viewArea)
        {
            _configs = configs.ToDictionary(cfg => cfg.Type);
            _viewArea = viewArea;
            _pools = Enum.GetValues(typeof(AsteroidType))
                .Cast<AsteroidType>()
                .ToDictionary<AsteroidType, AsteroidType, Pool<Asteroid>>(
                    type => type,
                    type => new Pool<Asteroid>(
                        _configs[type].Prefab, 
                        null, 
                        container, 
                        asteroid => Initialize(asteroid, type)));
        }

        public event Action<Asteroid> Destroyed = delegate { };

        void ISpawner<Asteroid>.Spawn(int count, Vector2 origin)
        {
            for (int i = 0; i < count; i++)
            {
                CreateAsteroid(_configs[AsteroidType.Big], origin);
            }
        }

        private void CreateAsteroid(AsteroidConfig config, Vector2 origin)
        {
            Asteroid asteroid = _pools[config.Type].Get();
            asteroid.transform.position = origin;
            asteroid.RandomizeOffset();
            asteroid.GetComponent<SpriteRenderer>().sprite =
                config.Sprites[Random.Range(0, config.Sprites.Count)];
        }

        private void Initialize(Asteroid asteroid, AsteroidType type)
        {
            AsteroidConfig config = _configs[type];
            asteroid.Initialize(
                new Wraparound<Asteroid>(asteroid, _viewArea), 
                new ConsistentMovement(
                    asteroid, 
                    Random.Range(config.Speed.Min, config.Speed.Max),
                    Vector2.one.RandomDirection()), 
                config);
            asteroid.Destroyed += () => Destroy(asteroid);
        }



        private void Destroy(Asteroid asteroid)
        {
            Vector2 asteroidPosition = asteroid.transform.position;
            switch (asteroid.Config.Type)
            {
                case AsteroidType.Big:
                    CreateAsteroid(_configs[AsteroidType.Medium], asteroidPosition);
                    CreateAsteroid(_configs[AsteroidType.Medium], asteroidPosition);
                    break;
                case AsteroidType.Medium:
                    CreateAsteroid(_configs[AsteroidType.Small], asteroidPosition);
                    CreateAsteroid(_configs[AsteroidType.Small], asteroidPosition);
                    break;
            }
            Destroyed(asteroid);
        }
    }
}
