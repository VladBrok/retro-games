using UnityEngine;
using SnakeGame.Input;

namespace SnakeGame.MonoBehaviours.GameScene
{
    public class Pauser : MonoBehaviour
    {
        [SerializeField] private GameUI _ui;

        private IPauseInput _input;

        public void Initialize(IPauseInput input)
        {
            _input = input;
            _ui.ContinueButtonClicked.AddListener(Unpause);
            _ui.QuitButtonClicked.AddListener(Unpause);
        }

        private void Update()
        {
            if (_input.Pause)
            {
                Pause();
            }
        }

        private void Pause()
        {
            _ui.SetPauseActive(true);
            Time.timeScale = 0f;
        }

        private void Unpause()
        {
            _ui.SetPauseActive(false);
            Time.timeScale = 1f;
        }
    }
}
