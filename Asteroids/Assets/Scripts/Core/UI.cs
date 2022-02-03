using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids
{
    [DisallowMultipleComponent]
    public class UI : MonoBehaviour
    {
        [SerializeField] private HorizontalLayoutGroup _livesPanel;
        [SerializeField] private Image _lifePrefab;
        [SerializeField] private Text _scoreValue;
        [SerializeField] private Text _highScoreValue;
        [SerializeField] private Canvas _gameOverCanvas;
        [SerializeField] private Button _restartButton;

        private ILifeController _player;
        private Stack<Image> _lives;

        public Button.ButtonClickedEvent RestartButtonClicked
        {
            get { return _restartButton.onClick; }
        }

        public void Initialize(ILifeController player)
        {
            _player = player;
            _player.LostLife += OnPlayerLostLife;
            _player.Dead += OnPlayerDead;

            _lives = new Stack<Image>(
                Enumerable
                    .Range(0, player.CurrentLives)
                    .Select(_ => Instantiate(_lifePrefab, _livesPanel.transform)));

            _gameOverCanvas.gameObject.SetActive(false);
        }

        public void UpdateScore(int score)
        {
            _scoreValue.text = score.ToString();
        }

        public void UpdateHighScore(int highScore)
        {
            _highScoreValue.text = highScore.ToString();
        }

        private void OnPlayerLostLife()
        {
            _lives.Pop().gameObject.SetActive(false);
        }

        private void OnPlayerDead()
        {
            _gameOverCanvas.gameObject.SetActive(true);
        }
    }
}
