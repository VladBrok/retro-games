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
        private int _lifeIndex;
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

        public void AddOneLife()
        {
            if (_lifeIndex + 1 >= _lives.Count) return;

            _lives[++_lifeIndex].gameObject.SetActive(true);
        }

        private void Awake()
        {
            Brick.Destroyed += OnBrickDestroyed;
            _floor.BallDead += OnBallDestroyed;
            _waitForLevelDisplay = new WaitForSeconds(_levelDisplayTimeInSeconds);
            _lifeIndex = _lives.Count - 1;
        }

        private void OnDestroy()
        {
            Brick.Destroyed -= OnBrickDestroyed;
        }

        private void OnBrickDestroyed(BrickDestroyedData _)
        {
            _score += 1;
            _scoreText.text = _score.ToString();
        }

        private void OnBallDestroyed()
        {
            RemoveOneLife();
            if (_lifeIndex < 0)
            {
                _gameOverCanvas.gameObject.SetActive(true);
                _paddle.gameObject.SetActive(false);
            }
        }

        private void RemoveOneLife()
        {
            _lives[_lifeIndex--].gameObject.SetActive(false);
        }

        private IEnumerator ShowLevelCanvasRoutine()
        {
            _levelCanvas.gameObject.SetActive(true);
            yield return _waitForLevelDisplay;
            _levelCanvas.gameObject.SetActive(false);
        }
    }
}
