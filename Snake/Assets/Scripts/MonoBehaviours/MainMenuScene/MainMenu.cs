using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using SnakeGame.MonoBehaviours.Shared;

namespace SnakeGame.MonoBehaviours.MainMenuScene
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private MainMenuUI _ui;

        private void Start()
        {
            _ui.PlayButtonClicked.AddListener(Play);
            _ui.QuitButtonClicked.AddListener(Quit);
        }

        private void Play()
        {
            SceneManager.LoadSceneAsync(SharedConstants.GameSceneIndex);
        }

        private void Quit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
