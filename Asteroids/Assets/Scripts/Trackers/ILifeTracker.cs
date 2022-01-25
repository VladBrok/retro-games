using System;

namespace Asteroids
{
    public interface ILifeTracker
    {
        event Action Dead;
        event Action LostLife;
        int CurrentLives { get; }
    }
}
