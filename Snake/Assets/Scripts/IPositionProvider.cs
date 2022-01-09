using UnityEngine;

namespace SnakeGame
{
    public interface IPositionProvider
    {
        Vector2 Position { get; set; }
    }
}
