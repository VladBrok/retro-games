using UnityEngine;
using UnityEngine.UI;

namespace SnakeGame.MonoBehaviours.MainMenuScene
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;

        public Button.ButtonClickedEvent PlayButtonClicked
        {
            get { return _playButton.onClick; }
        }

        public Button.ButtonClickedEvent QuitButtonClicked
        {
            get { return _quitButton.onClick; }
        }
    }
}
