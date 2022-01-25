using System;

namespace Asteroids
{
    public class LifeTracker : ILifeTracker
    {
        private readonly IDestructible _target;
        private int _currentLives;

        public LifeTracker(IDestructible target, int maxLives)
        {
            if (maxLives < 1) throw new ArgumentOutOfRangeException("maxLives");

            _target = target;
            _currentLives = maxLives;
            _target.Destroyed += OnTargetDestroyed;
        }

        public event Action Dead = delegate { };
        public event Action LostLife = delegate { };

        public int CurrentLives
        {
            get { return _currentLives; }
        }

        private void OnTargetDestroyed()
        {
            _currentLives--;
            LostLife();
            if (_currentLives == 0)
            {
                Dead();
                _target.Destroyed -= OnTargetDestroyed;
            }
        }
    }
}
