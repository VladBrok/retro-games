using System;
using UnityEngine;

namespace SnakeGame
{
    public interface ITrigger
    {
        event Action TriggerEntered;

        void OnTriggerEnter2D(Collider2D other);
    }
}
