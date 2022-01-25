﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Asteroids
{
    public class UI : MonoBehaviour
    {
        [SerializeField] private HorizontalLayoutGroup _livesPanel;
        [SerializeField] private Image _lifePrefab;
        [SerializeField] private Text _scoreText;
        [SerializeField] private Text _highScoreText;
        [SerializeField] private Text _gameOverText;
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
                Enumerable.Range(0, player.CurrentLives)
                    .Select(_ => Instantiate(_lifePrefab, _livesPanel.transform)));
            
            SetGameOverActive(false);
        }

        public void UpdateScore(int score)
        {
            _scoreText.text = "Score: " + score;
        }

        public void UpdateHighScore(int highScore)
        {
            _highScoreText.text = "HighScore: " + highScore;
        }

        private void OnPlayerLostLife()
        {
            _lives.Pop().gameObject.SetActive(false);
        }

        private void OnPlayerDead()
        {
            SetGameOverActive(true);
        }

        private void SetGameOverActive(bool active)
        {
            _gameOverText.gameObject.SetActive(active);
            _restartButton.gameObject.SetActive(active);
        }
    }
}
