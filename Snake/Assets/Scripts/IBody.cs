using UnityEngine;

namespace SnakeGame
{
    public interface IBody
    {
        Vector2 Position { get; set; }
        Vector2 Size { get; }
    }
}