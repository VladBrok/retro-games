using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Asteroids.Extensions;

namespace Asteroids
{
    [RequireComponent(typeof(Pauser))]
    [RequireComponent(typeof(Respawner))]
    [RequireComponent(typeof(ISoundEffectsPlayer))]
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
        private ISoundEffectsPlayer _sfxPlayer;
        private Camera _camera;
        private Bounds _cameraView;

        private void Awake()
        {
            _gameController = new GameController(_gameConfig, _ui, _impactParticle);
            _sfxPlayer = GetComponent<ISoundEffectsPlayer>();
            _camera = Camera.main;
            _cameraView = _camera.GetViewBounds2D();

            Transform shipContainer, projectileContainer, asteroidContainer;
            CreateObjectsHierarchy(
                out asteroidContainer, 
                out shipContainer, 
                out projectileContainer);

            var input = CreateInput();
            var player = CreatePlayer(shipContainer, projectileContainer, input);
            var asteroidPools = CreateAsteroidPools(asteroidContainer);
            var enemyPool = CreateEnemyPool(shipContainer, projectileContainer);

            CreateAsteroidController(player, asteroidPools);
            CreateEnemyController(enemyPool);
            var playerLifeController = CreatePlayerLifeController(player);

            _ui.Initialize(playerLifeController);
            _ui.RestartButtonClicked.AddListener(_gameController.Restart);

            InitializePauser(input, player);
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

        private KeyboardInput CreateInput()
        {
            return new KeyboardInput();
        }

        private PlayerShip CreatePlayer(
            Transform shipContainer,
            Transform projectileContainer,
            KeyboardInput input)
        {
            var respawner = GetComponent<Respawner>();
            PlayerShip player = Instantiate(_playerPrefab, shipContainer);
            player.transform.position = Vector3.zero;
            player.Initialize(
                new Wraparound<PlayerShip>(player, _cameraView),
                _cameraView,
                projectileContainer,
                respawner,
                input,
                _sfxPlayer);
            return player;
        }

        private Dictionary<AsteroidType, IPool<Asteroid>> CreateAsteroidPools(
            Transform asteroidContainer)
        {
            InitializeAsteroidConfigs();

            Transform spawnPoint = null;
            var pools = _asteroidConfigs
                .ToDictionary<AsteroidConfig, AsteroidType, IPool<Asteroid>>(
                    c => c.Type,
                    c => new Pool<Asteroid>(
                            c.Prefab,
                            spawnPoint,
                            asteroidContainer,
                            a => InitializeAsteroid(a, c)),
                    new AsteroidTypeEqualityComparer());
            return pools;
        }

        private Pool<EnemyShip> CreateEnemyPool(
            Transform shipContainer, 
            Transform projectileContainer)
        {
            Transform spawnPoint = null;
            var pool = new Pool<EnemyShip>(
                _enemyPrefab,
                spawnPoint,
                shipContainer,
                e => InitializeEnemy(e, projectileContainer));
            return pool;
        }

        private void CreateAsteroidController(
            PlayerShip player, 
            Dictionary<AsteroidType, IPool<Asteroid>> pools)
        {
            new AsteroidController<Asteroid>(
                pools, 
                player, 
                _gameConfig.InitialBigAsteroidCount);
        }

        private void CreateEnemyController(Pool<EnemyShip> pool)
        {
            var coroutineStarter = this;
            var spawnDelay = new Value(5, 30);
            new EnemyController<EnemyShip>(
                pool, 
                coroutineStarter, 
                _cameraView, 
                spawnDelay);
        }

        private LifeController CreatePlayerLifeController(PlayerShip player)
        {
            var playerLifeController = new LifeController(player, _gameConfig.PlayerLives);
            playerLifeController.Dead += () =>
                StartCoroutine(_gameController.PlayerDeathRoutine());
            return playerLifeController;
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

        private void InitializeEnemy(EnemyShip enemy, Transform projectileContainer)
        {
            enemy.Initialize(
                new Wraparound<EnemyShip>(enemy, _cameraView),
                new ConsistentMovement(
                    enemy,
                    _enemyConfig.Speed.Randomize(),
                    Vector2.zero),
                projectileContainer,
                _cameraView,
                _camera,
                _sfxPlayer);
            enemy.Destroyed += () =>
                _gameController.HandleDestroyOf(enemy, _enemyConfig.Score);
        }

        private void InitializeAsteroid(Asteroid asteroid, AsteroidConfig config)
        {
            asteroid.Initialize(
                new Wraparound<Asteroid>(asteroid, _cameraView),
                new ConsistentMovement(
                    asteroid,
                    config.Speed.Randomize(),
                    Vector2.one.RandomDirection()),
                config,
                _sfxPlayer);
            asteroid.Destroyed += () =>
                _gameController.HandleDestroyOf(asteroid, asteroid.Config.Score);
        }

        private void InitializePauser(KeyboardInput input, PlayerShip player)
        {
            GetComponent<Pauser>().Initialize(
                input,
                new PausableAudio(GetComponent<AudioSource>()),
                player);
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
