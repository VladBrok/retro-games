using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Arkanoid.Controllers
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private Slider _progressBar;
        [SerializeField] private Canvas _loadingCanvas;

        public void LoadScene(string name)
        {
            _loadingCanvas.gameObject.SetActive(true);
            StartCoroutine(
                LoadSceneRoutine(name, whenLoaded: () => _loadingCanvas.gameObject.SetActive(false)));
        }

        private IEnumerator LoadSceneRoutine(string name, Action whenLoaded)
        {
            AsyncOperation loading = SceneManager.LoadSceneAsync(name);
            yield return UpdateProgressBar(loading);
            whenLoaded();
        }

        private IEnumerator UpdateProgressBar(AsyncOperation loading)
        {
            _progressBar.value = 0f;
            while (!loading.isDone)
            {
                _progressBar.value = loading.progress;
                yield return null;
            }
            _progressBar.value = loading.progress;
        }
    }
}
