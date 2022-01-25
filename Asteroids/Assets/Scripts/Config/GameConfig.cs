using UnityEngine;

namespace Asteroids
{
    [CreateAssetMenu(menuName = "Config/Game")]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private int _playerLives;
        [SerializeField] private int _initialBigAsteroidCount;
        [SerializeField] private float _pauseDelayInSeconds;

        public int PlayerLives
        {
            get { return _playerLives; }
        }
        public int InitialBigAsteroidCount
        {
            get { return _initialBigAsteroidCount; }
        }
        public float PauseDelayInSeconds
        {
            get { return _pauseDelayInSeconds; }
        }
    }
}
