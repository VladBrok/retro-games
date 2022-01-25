using UnityEngine;

namespace Asteroids
{
    public interface IMovement
    {
        Vector2 Direction { set; }
        void Move(float deltaTime);
    }
}
