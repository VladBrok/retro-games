using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Asteroids
{
    public class GameController
    {
        private readonly UI _ui;
        private readonly ParticleSystem _impactParticle;
        private readonly ISaveSystem _saveSystem;
        private readonly WaitForSeconds _waitForAfterDeathPause;
        private SaveData _data;
        private int _score;

        public GameController(GameConfig config, UI ui, ParticleSystem impactParticle)
        {
            _ui = ui;
            _impactParticle = impactParticle;
            _saveSystem = new JsonSaveSystem("data.txt");
            _waitForAfterDeathPause = new WaitForSeconds(config.PauseAfterPlayerDeathInSeconds);

            _data = _saveSystem.Load();
            _ui.UpdateHighScore(_data.HighScore);
        }

        public IEnumerator PlayerDeathRoutine()
        {
            yield return _waitForAfterDeathPause;
            Time.timeScale = 0f;
        }

        public void Restart()
        {
            _saveSystem.Save(_data);
            SceneManager.LoadSceneAsync((int)Scene.Game);
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
