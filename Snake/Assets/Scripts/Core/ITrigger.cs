using System;
using UnityEngine;

namespace SnakeGame
{
    public interface ITrigger
    {
        event Action TriggerEntered;
    }
}
