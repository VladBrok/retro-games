using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Arkanoid
{
    public class UI : MonoBehaviour
    {
        [SerializeField] [Range(0.1f, 5f)] private float _levelDisplayTimeInSeconds;
        [SerializeField] private Floor _floor;
        [SerializeField] private Paddle _paddle;
        [SerializeField] private Text _scoreText;
        [SerializeField] private Text _levelText;
        [SerializeField] private Canvas _gameOverCanvas;
        [SerializeField] private Canvas _victoryCanvas;
        [SerializeField] private Canvas _levelCanvas;
        [SerializeField] private List<Image> _lives;

        private int _score;
        private WaitForSeconds _waitForLevelDisplay;

        public IEnumerator UpdateLevelRoutine(int level)
        {
            _levelText.text = level.ToString();
            yield return ShowLevelCanvasRoutine();
        }

        public void ShowVictoryCanvas()
        {
            _victoryCanvas.gameObject.SetActive(true);
        }

        private void Awake()
        {
            Brick.Destroyed += UpdateScore;
            _floor.BallDead += UpdateLives;
            _waitForLevelDisplay = new WaitForSeconds(_levelDisplayTimeInSeconds);
        }

        private void OnDestroy()
        {
            Brick.Destroyed -= UpdateScore;
        }

        private void UpdateScore()
        {
            _score += 1;
            _scoreText.text = _score.ToString();
        }

        private void UpdateLives()
        {
            RemoveOneLife();
            if (_lives.Count == 0)
            {
                _gameOverCanvas.gameObject.SetActive(true);
                _paddle.gameObject.SetActive(false);
            }
        }

        private void RemoveOneLife()
        {
            int last = _lives.Count - 1;
            Destroy(_lives[last]);
            _lives.RemoveAt(last);
        }

        private IEnumerator ShowLevelCanvasRoutine()
        {
            _levelCanvas.gameObject.SetActive(true);
            yield return _waitForLevelDisplay;
            _levelCanvas.gameObject.SetActive(false);
        }
    }
}
