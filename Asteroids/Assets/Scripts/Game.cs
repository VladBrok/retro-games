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
        private readonly int _mainSceneIndex = 0;
        
        [SerializeField] private UI _ui;
        [SerializeField] private ParticleSystem _impactParticle;
        [SerializeField] private AsteroidConfig[] _asteroidConfigs;
        [SerializeField] private PlayerShip _playerPrefab;
        [SerializeField] private EnemyShip _enemyPrefab;
        [SerializeField] private int _playerLives;
        [SerializeField] private float _pauseDelayInSeconds;
        [SerializeField] private int _initialBigAsteroidCount;

        private WaitForSeconds _waitForPauseDelay;
        private ISaveSystem _saveSystem;
        private SaveData _data;
        private int _score;

        private void Awake()
        {
            _saveSystem = new JsonSaveSystem("data.txt");
            _data = _saveSystem.Load();

            Bounds cameraView = Camera.main.GetViewBounds2D();
            _waitForPauseDelay = new WaitForSeconds(_pauseDelayInSeconds);

            Transform shipContainer, projectileContainer, asteroidContainer;
            CreateObjectsHierarchy(out asteroidContainer, out shipContainer, out projectileContainer);

            PlayerShip player = Instantiate(_playerPrefab, shipContainer);
            player.Initialize(
                new Wraparound<PlayerShip>(player, cameraView), 
                cameraView, 
                projectileContainer);

            var playerLifeTracker = new LifeTracker(player, _playerLives);
            playerLifeTracker.Dead += () => StartCoroutine(Pause());

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
            asteroidSpawner.Destroyed += a => OnDestroyed(a, a.Config.Score);

            new AsteroidTracker<Asteroid>(
                asteroidSpawner, player, _initialBigAsteroidCount);

            var enemySpawner = new EnemySpawner(
                _enemyPrefab, shipContainer, projectileContainer, cameraView, Camera.main);
            enemySpawner.Destroyed += _ => OnDestroyed(_, 50);

            new EnemyTracker<EnemyShip>(
                enemySpawner, this, cameraView, new Value(5, 30));

            _ui.Initialize(playerLifeTracker);
            _ui.RestartButtonClicked.AddListener(Restart);
            _ui.UpdateHighScore(_data.HighScore);
        }

        private IEnumerator Pause()
        {
            yield return _waitForPauseDelay;
            Time.timeScale = 0f;
        }

        private void Restart()
        {
            _saveSystem.Save(_data);
            SceneManager.LoadSceneAsync(_mainSceneIndex);
            Time.timeScale = 1f;
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

        private void OnDestroyed<T>(Destructible<T> entity, int scoreGain)
            where T : IWrapable
        {
            UpdateParticles<T>(entity);
            UpdateScores(scoreGain);
            UpdateUI();
        }

        private void UpdateParticles<T>(Destructible<T> entity)
            where T : IWrapable
        {
            _impactParticle.transform.position = entity.transform.position;
            _impactParticle.Play();
        }

        private void UpdateScores(int gain)
        {
            _score += gain;
            _data.HighScore = Math.Max(_score, _data.HighScore);
        }

        private void UpdateUI()
        {
            _ui.UpdateScore(_score);
            _ui.UpdateHighScore(_data.HighScore);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus) return;

            _saveSystem.Save(_data);
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
