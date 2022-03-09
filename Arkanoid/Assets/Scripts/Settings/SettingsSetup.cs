using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Arkanoid
{
    public class SettingsSetup : MonoBehaviour
    {
        [SerializeField] private List<DifficultySettings> _difficultySettings;
        [SerializeField] private Ball _ball;
        [SerializeField] private Paddle _paddle;

        private void Awake()
        {
            SetupDifficultySettings();
        }

        private void SetupDifficultySettings()
        {
            DifficultySettings selected = _difficultySettings.Single(s => s.Selected);
            _ball.Initialize(selected.BallSpeed);
            _paddle.transform.localScale = new Vector3(
                _paddle.transform.localScale.x * selected.PaddleWidthMultiplier,
                _paddle.transform.localScale.y,
                _paddle.transform.localScale.z);
        }
    }
}
