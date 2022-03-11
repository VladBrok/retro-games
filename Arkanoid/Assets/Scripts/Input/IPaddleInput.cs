using UnityEngine;

namespace Arkanoid.Input
{
    public interface IPaddleInput
    {
        Vector2 MovementDirection { get; }
        bool LaunchBall { get; }
    }
}
