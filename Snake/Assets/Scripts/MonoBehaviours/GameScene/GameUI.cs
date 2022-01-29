using UnityEngine;
using UnityEngine.UI;

namespace SnakeGame.MonoBehaviours.GameScene
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Text _scoreText;
        [SerializeField] private Canvas _pauseCanvas;
        [SerializeField] private Canvas _gameOverCanvas;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Button _restartButton;

        private int _score;

        public Button.ButtonClickedEvent ContinueButtonClicked
        {
            get { return _continueButton.onClick; }
        }
        public Button.ButtonClickedEvent QuitButtonClicked
        {
            get { return _quitButton.onClick; }
        }
        public Button.ButtonClickedEvent MainMenuButtonClicked
        {
            get { return _mainMenuButton.onClick; }
        }
        public Button.ButtonClickedEvent RestartButtonClicked
        {
            get { return _restartButton.onClick; }
        }

        public void Initialize(ITrigger food)
        {
            food.TriggerEntered += UpdateScore;
        }

        public void SetPauseActive(bool active)
        {
            _pauseCanvas.gameObject.SetActive(active);
        }

        public void SetGameOverActive(bool active)
        {
            _gameOverCanvas.gameObject.SetActive(active);
        }

        private void UpdateScore()
        {
            _score++;
            _scoreText.text = "x " + _score;
        }
    }
}
