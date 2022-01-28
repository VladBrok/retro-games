using UnityEngine;
using UnityEngine.UI;

namespace SnakeGame.MonoBehaviours
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private Text _scoreText;
        [SerializeField] private Canvas _pauseCanvas;
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _quitButton;

        private int _score;

        public Button.ButtonClickedEvent ContinueButtonClicked
        {
            get { return _continueButton.onClick; }
        }

        public Button.ButtonClickedEvent QuitButtonClicked
        {
            get { return _quitButton.onClick; }
        }

        public void Initialize(ITrigger food)
        {
            food.TriggerEntered += UpdateScore;
        }

        public void SetPauseActive(bool active)
        {
            _pauseCanvas.gameObject.SetActive(active);
        }

        private void UpdateScore()
        {
            _score++;
            _scoreText.text = "x " + _score;
        }
    }
}
