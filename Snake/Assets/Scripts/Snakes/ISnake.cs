using UnityEngine;

namespace SnakeGame.Snakes
{
    public interface ISnake
    {
        Vector2 MovementDirection { get; }
        IBody Head { get; }
        IBody Tip { get; }

        void Move();
        void ChangeMovementDirection(Vector2 newDirection);
        void AddBody(IBody item);
    }
}
