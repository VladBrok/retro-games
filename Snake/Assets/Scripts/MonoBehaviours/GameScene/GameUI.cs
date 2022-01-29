using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace SnakeGame.MonoBehaviours.GameScene
{
    public class GameUI : MonoBehaviour
    {
        [SerializeField] private Text _scoreText;
        [SerializeField] private Canvas _pauseCanvas;
        [SerializeField] private Canvas _gameOverCanvas;
        [SerializeField] private Canvas _victoryCanvas;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _playAgainButton;
        [SerializeField] private Button[] _mainMenuButtons;

        private int _score;

        public Button.ButtonClickedEvent ContinueButtonClicked
        {
            get { return _continueButton.onClick; }
        }
        public Button.ButtonClickedEvent RestartButtonClicked
        {
            get { return _restartButton.onClick; }
        }
        public Button.ButtonClickedEvent PlayAgainButtonClicked
        {
            get { return _playAgainButton.onClick; }
        }
        public Button.ButtonClickedEvent[] MainMenuButtonClicked
        {
            get { return _mainMenuButtons.Select(b => b.onClick).ToArray(); }
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

        public void SetVictoryActive(bool active)
        {
            _victoryCanvas.gameObject.SetActive(active);
        }

        private void UpdateScore()
        {
            _score++;
            _scoreText.text = "x " + _score;
        }
    }
}
