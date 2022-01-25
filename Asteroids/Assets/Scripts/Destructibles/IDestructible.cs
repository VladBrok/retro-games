using System;

namespace Asteroids
{
    public interface IDestructible
    {
        event Action Destroyed;
        void Destroy();
    }
}
