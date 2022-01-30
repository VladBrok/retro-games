using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Asteroids.Extensions;

namespace Asteroids
{
    [RequireComponent(typeof(Respawner))]
    [DisallowMultipleComponent]
    public sealed class Game : MonoBehaviour, ICoroutineStarter
    {        
        [SerializeField] private UI _ui;
        [SerializeField] private GameConfig _gameConfig;
        [SerializeField] private AsteroidConfig[] _asteroidConfigs;
        [SerializeField] private ObjectConfig _enemyConfig;
        [SerializeField] private ParticleSystem _impactParticle;
        [SerializeField] private PlayerShip _playerPrefab;
        [SerializeField] private EnemyShip _enemyPrefab;

        private GameController _gameController;
        private Bounds _cameraView;

        private void Awake()
        {
            _gameController = new GameController(_gameConfig, _ui, _impactParticle);
            _cameraView = Camera.main.GetViewBounds2D();

            Transform shipContainer, projectileContainer, asteroidContainer;
            CreateObjectsHierarchy(
                out asteroidContainer, 
                out shipContainer, 
                out projectileContainer);

            var player = CreatePlayer(shipContainer, projectileContainer);
            var asteroidSpawner = CreateAsteroidSpawner(asteroidContainer);
            var enemySpawner = CreateEnemySpawner(shipContainer, projectileContainer);

            CreateAsteroidController(player, asteroidSpawner);
            CreateEnemyController(enemySpawner);
            var playerLifeController = CreatePlayerLifeController(player);

            _ui.Initialize(playerLifeController);
            _ui.RestartButtonClicked.AddListener(_gameController.Restart);
        }

        private void CreateEnemyController(EnemySpawner enemySpawner)
        {
            new EnemyController<EnemyShip>(
                enemySpawner, this, _cameraView, new Value(5, 30));
        }

        private EnemySpawner CreateEnemySpawner(
            Transform shipContainer, 
            Transform projectileContainer)
        {
            var enemySpawner = new EnemySpawner(
                _enemyPrefab, 
                shipContainer, 
                projectileContainer,
                _cameraView, 
                Camera.main,
                _enemyConfig);
            enemySpawner.Destroyed += e =>
                _gameController.HandleDestroyOf(e, _enemyConfig.Score);
            return enemySpawner;
        }

        private void CreateAsteroidController(
            PlayerShip player, 
            AsteroidSpawner asteroidSpawner)
        {
            new AsteroidController<Asteroid>(
                asteroidSpawner, player, _gameConfig.InitialBigAsteroidCount);
        }

        private AsteroidSpawner CreateAsteroidSpawner(Transform asteroidContainer)
        {
            InitializeAsteroidConfigs();

            var asteroidSpawner = new AsteroidSpawner(
                _asteroidConfigs, asteroidContainer, _cameraView);
            asteroidSpawner.Destroyed += a => 
                _gameController.HandleDestroyOf(a, a.Config.Score);
            return asteroidSpawner;
        }

        private void InitializeAsteroidConfigs()
        {
            Vector2 viewExtents = _cameraView.extents;
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
        }

        private LifeController CreatePlayerLifeController(PlayerShip player)
        {
            var playerLifeController = new LifeController(player, _gameConfig.PlayerLives);
            playerLifeController.Dead += () => StartCoroutine(_gameController.PauseRoutine());
            return playerLifeController;
        }

        private PlayerShip CreatePlayer(Transform shipContainer, Transform projectileContainer)
        {
            var respawner = GetComponent<Respawner>();
            PlayerShip player = Instantiate(_playerPrefab, shipContainer);
            player.Initialize(
                new Wraparound<PlayerShip>(player, _cameraView),
                _cameraView,
                projectileContainer,
                respawner);
            return player;
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
            _gameController.HandleApplicationFocus(hasFocus);
        }

        private void OnValidate()
        {
            var asteroidTypes = Enum.GetValues(typeof(AsteroidType)).Cast<AsteroidType>();
            Debug.Assert(
                _asteroidConfigs.Select(c => c.Type).SequenceEqual(asteroidTypes),
                "Asteroid configs should have one config for each asteroid type.");
        }
    }
}
