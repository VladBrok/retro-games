using System;
using UnityEngine;

namespace Asteroids
{
    public interface ISpawner<T> where T : IDestructible
    {
        event Action<T> Destroyed;
        void Spawn(int count, Vector2 origin);
    }
}
