using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Asteroids.Extensions;

namespace Asteroids
{
    [DisallowMultipleComponent]
    public sealed class Game : MonoBehaviour, ICoroutineStarter
    {        
        [SerializeField] private UI _ui;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private AsteroidConfig[] _asteroidConfigs;
        [SerializeField] private ParticleSystem _impactParticle;
        [SerializeField] private PlayerShip _playerPrefab;
        [SerializeField] private EnemyShip _enemyPrefab;

        private GameController _controller;

        private void Awake()
        {
            _controller = new GameController(_gameConfig, _ui, _impactParticle);
            Bounds cameraView = Camera.main.GetViewBounds2D();

            Transform shipContainer, projectileContainer, asteroidContainer;
            CreateObjectsHierarchy(out asteroidContainer, out shipContainer, out projectileContainer);

            PlayerShip player = Instantiate(_playerPrefab, shipContainer);
            player.Initialize(
                new Wraparound<PlayerShip>(player, cameraView), 
                cameraView, 
                projectileContainer);

            var playerLifeTracker = new LifeTracker(player, _gameConfig.PlayerLives);
            playerLifeTracker.Dead += () => StartCoroutine(_controller.PauseRoutine());

            Vector2 viewExtents = cameraView.extents;
            foreach (AsteroidConfig config in _asteroidConfigs)
            {
                var spawnOffsetX = new Value(
                    viewExtents.x.Percentage(config.SpawnOffsetPercent.Min),
                    viewExtents.x.Percentage(config.SpawnOffsetPercent.Max));
                var spawnOffsetY = new Value(
                    viewExtents.y.Percentage(config.SpawnOffsetPercent.Min),
                    viewExtents.y.Percentage(config.SpawnOffsetPercent.Max));
                config.Initialize(spawnOffsetX, spawnOffsetY);
            }
            var asteroidSpawner = new AsteroidSpawner(
                _asteroidConfigs, asteroidContainer, cameraView);
            asteroidSpawner.Destroyed += a => _controller.HandleDestroyOf(a, a.Config.Score);

            new AsteroidTracker<Asteroid>(
                asteroidSpawner, player, _gameConfig.InitialBigAsteroidCount);

            var enemySpawner = new EnemySpawner(
                _enemyPrefab, shipContainer, projectileContainer, cameraView, Camera.main);
            enemySpawner.Destroyed += e => _controller.HandleDestroyOf(e, 50);

            new EnemyTracker<EnemyShip>(
                enemySpawner, this, cameraView, new Value(5, 30));

            _ui.Initialize(playerLifeTracker);
            _ui.RestartButtonClicked.AddListener(_controller.Restart);
        }

        private void CreateObjectsHierarchy(
            out Transform asteroids, 
            out Transform ships,
            out Transform projectiles)
        {
            var parent = new GameObject("_Dynamic").transform;
            asteroids = new GameObject("Asteroids").transform;
            ships = new GameObject("Ships").transform;
            projectiles = new GameObject("Projectiles").transform;
            asteroids.parent = ships.parent = projectiles.parent = parent;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            _controller.HandleApplicationFocus(hasFocus);
        }

        private void OnValidate()
        {
            var asteroidTypes = Enum.GetValues(typeof(AsteroidType)).Cast<AsteroidType>();
            Debug.Assert(
                _asteroidConfigs
                    .Select(config => config.Type)
                    .SequenceEqual(asteroidTypes),
                "Asteroid configs should have one config for each asteroid type.");
        }
    }
}
