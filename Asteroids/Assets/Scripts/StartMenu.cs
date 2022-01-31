using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Asteroids
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private Button _newGameButton;
        [SerializeField] private Animator _loadingAnimator;

        private void Awake()
        {
            _loadingAnimator.gameObject.SetActive(false);
            _newGameButton.onClick.AddListener(() => StartCoroutine(StartGameRoutine()));
        }

        private IEnumerator StartGameRoutine()
        {
            _loadingAnimator.gameObject.SetActive(true);
            _newGameButton.interactable = false;
            AsyncOperation loading = SceneManager.LoadSceneAsync((int)Scene.Main);
            while (!loading.isDone)
            {
                yield return null;
            }
        }
    }
}
