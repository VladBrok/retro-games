using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Config/Game")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField, Range(0, 100)] private int _playerLives;
        [SerializeField, Range(1, 100)] private int _initialBigAsteroidCount;
        [SerializeField, Range(0, 10)] private float _pauseAfterPlayerDeathInSeconds;

        public int PlayerLives
        {
            get { return _playerLives; }
        }
        public int InitialBigAsteroidCount
        {
            get { return _initialBigAsteroidCount; }
        }
        public float PauseAfterPlayerDeathInSeconds
        {
            get { return _pauseAfterPlayerDeathInSeconds; }
        }
    }
}
