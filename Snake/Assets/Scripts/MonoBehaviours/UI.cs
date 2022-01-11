using UnityEngine;
using UnityEngine.UI;

namespace SnakeGame.MonoBehaviours
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private Text _scoreText;

        private int _score;

        public void Initialize(ITrigger food)
        {
            food.TriggerEntered += UpdateScore;
        }

        public void GameOver()
        {
            Debug.Log("<color=red>GameOver.</color>");
        }

        private void UpdateScore()
        {
            _score++;
            _scoreText.text = "x " + _score;
        }
    }
}
