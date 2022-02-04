using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Asteroids.Extensions;

namespace Asteroids
{
    [DisallowMultipleComponent]
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Animator _loadingAnimator;
        [SerializeField] private AsteroidConfig[] _asteroidConfigs;
        [SerializeField] [Range(3, 30)] private int _asteroidsToSpawn;

        private void Awake()
        {
            SpawnAsteroids(Camera.main.GetViewBounds2D());
            _loadingAnimator.gameObject.SetActive(false);
            _newGameButton.onClick.AddListener(() => StartCoroutine(StartGameRoutine()));
        }

        private void SpawnAsteroids(Bounds viewArea)
        {
            for (int i = 0; i < _asteroidsToSpawn; i++)
            {
                var config = _asteroidConfigs.TakeRandom();
                Asteroid asteroid = Instantiate(
                    config.Prefab,
                    new Vector2(
                        Random.Range(viewArea.min.x, viewArea.max.x),
                        Random.Range(viewArea.min.y, viewArea.max.y)),
                    Quaternion.identity);
                asteroid.Initialize(
                    new Wraparound<Asteroid>(asteroid, viewArea),
                    new ConsistentMovement(
                        asteroid,
                        config.Speed.Min,
                        Vector2.one.RandomDirection()));
            }
        }

        private IEnumerator StartGameRoutine()
        {
            _loadingAnimator.gameObject.SetActive(true);
            _newGameButton.interactable = false;
            AsyncOperation loading = SceneManager.LoadSceneAsync((int)Scene.Game);
            while (!loading.isDone)
            {
                yield return null;
            }
        }
    }
}
