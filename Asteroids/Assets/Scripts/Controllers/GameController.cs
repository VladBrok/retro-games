using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids
{
    public class GameController
    {
        private readonly int _mainSceneIndex = 0;
        private readonly UI _ui;
        private readonly ParticleSystem _impactParticle;
        private readonly ISaveSystem _saveSystem;
        private readonly WaitForSeconds _waitForPauseDelay;
        private SaveData _data;
        private int _score;

        public GameController(GameConfig config, UI ui, ParticleSystem impactParticle)
        {
            _ui = ui;
            _impactParticle = impactParticle;
            _saveSystem = new JsonSaveSystem("data.txt");
            _waitForPauseDelay = new WaitForSeconds(config.PauseDelayInSeconds);

            _data = _saveSystem.Load();
            _ui.UpdateHighScore(_data.HighScore);
        }

        public IEnumerator PauseRoutine()
        {
            yield return _waitForPauseDelay;
            Time.timeScale = 0f;
        }

        public void Restart()
        {
            _saveSystem.Save(_data);
            SceneManager.LoadSceneAsync(_mainSceneIndex);
            Time.timeScale = 1f;
        }

        public void HandleApplicationFocus(bool hasFocus)
        {
            if (hasFocus) return;

            _saveSystem.Save(_data);
        }

        public void HandleDestroyOf(ICenterProvider entity, int scoreGain)
        {
            UpdateParticles(entity);
            UpdateScores(scoreGain);
            UpdateUI();
        }

        private void UpdateParticles(ICenterProvider entity)
        {
            _impactParticle.transform.position = entity.Center;
            _impactParticle.Play();
        }

        private void UpdateScores(int gain)
        {
            _score += gain;
            _data.HighScore = Math.Max(_score, _data.HighScore);
        }

        private void UpdateUI()
        {
            _ui.UpdateScore(_score);
            _ui.UpdateHighScore(_data.HighScore);
        }
    }
}
