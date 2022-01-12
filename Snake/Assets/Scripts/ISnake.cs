using UnityEngine;

namespace SnakeGame
{
    public interface ISnake
    {
        Vector2 MovementDirection { get; }

        void Move();
        void ChangeMovementDirection(Vector2 newDirection);
        void AddBody(IBody item);
    }
}
