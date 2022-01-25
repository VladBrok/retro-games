using System;

namespace Asteroids
{
    public interface ILifeController
    {
        event Action Dead;
        event Action LostLife;
        int CurrentLives { get; }
    }
}
