using System;
using UnityEngine;

namespace Asteroids
{
    public interface IPool<T>
    {
        event Action<T> ObjectDestroyed;

        T Get();
        T Get(Vector2 spawnPosition);
        void Get(int count, Vector2 spawnPosition);
    }
}
